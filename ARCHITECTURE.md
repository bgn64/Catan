# Catan Library Architecture

## Core Design: Nested Phase-Based Game State

The game state is modeled as a hierarchy of phases, where each phase contains state specific to that phase and determines what actions are valid.

### Phase Hierarchy

```
IGame
├── IGamePhase (CurrentPhase)
    ├── ISetupPhase
    │   └── ISetupSubPhase (CurrentStep)
    │       ├── IPlaceSettlementStep
    │       └── IPlaceRoadStep
    ├── ITurnPhase
    │   └── ITurnSubPhase (CurrentStep)
    │       ├── IPreRollStep
    │       ├── IDiscardStep
    │       ├── IRobberStep
    │       └── IPostRollStep
    └── IEndedPhase
```

### Visitor Pattern for Phase Discrimination

Each phase hierarchy uses the **Visitor Pattern** to handle phase-specific logic:

- **Base interface** (e.g., `IGamePhase`) defines `void Accept(IGamePhaseVisitor visitor)`
- **Visitor interface** (e.g., `IGamePhaseVisitor`) defines `void Visit(TPhase phase)` for each concrete phase
- **Concrete phases** implement `Accept()` by calling the appropriate `Visit()` method on the visitor

**Benefits:**
- Compile-time exhaustiveness checking (must handle all phases)
- Type-safe phase discrimination without casting
- Clean separation between phase state and phase-handling logic

**Usage:**
```csharp
// Exhaustive handling
public class UIRenderer : IGamePhaseVisitor, ITurnSubPhaseVisitor
{
    public void Visit(ISetupPhase phase) { /* ... */ }
    public void Visit(ITurnPhase phase) { /* ... */ }
    public void Visit(IEndedPhase phase) { /* ... */ }
    
    public void Visit(IPreRollStep step) { /* ... */ }
    // ... must implement all turn substeps
}

game.CurrentPhase.Accept(renderer);

// Ad-hoc checks (when you don't need exhaustiveness)
if (game.CurrentPhase is ITurnPhase turn)
{
    if (turn.CurrentStep is IPostRollStep postRoll)
    {
        // Do something specific to post-roll
    }
}
```

### Phase-Agnostic State

State that persists across all phases lives at the `IGame` level:
- `IBoard` - Hex grid, intersections, edges, harbors, robber location
- `IReadOnlyList<IPlayer>` - Player state (resources, victory points, etc.)
- Events for UI reactivity

## Player Actions

### Command Pattern with Behavior

Actions are modeled as command objects that encapsulate both data and execution logic:

```csharp
public interface IGame
{
    IEnumerable<IGameCommand> GetValidCommands();
    bool CanExecute(IGameCommand command);
    void Execute(IGameCommand command);
}

public interface IGameCommand 
{ 
    void Execute(IGame game);
    bool CanExecute(IGame game);
}

public class BuildRoadCommand : IGameCommand
{
    public EdgeCoordinate Location { get; set; }
    
    public void Execute(IGame game) { /* implementation */ }
    public bool CanExecute(IGame game) { /* validation */ }
}
// ... etc
```

**Design Decisions:**
- Commands contain both action data and execution logic (classic Command Pattern)
- Commands are the **only public API** for modifying game state
- Game state interfaces expose no direct action methods
- Game state implementations may have low-level utility methods, but not complete player actions

**Benefits:**
- Easy serialization (networking, save/load, replay)
- Single validation entry point
- AI can enumerate all legal moves
- Natural fit for undo/redo
- Clean separation: game state = data, commands = behavior

**Implementation:**
- `IGame.Execute()` dispatches to `command.Execute(this)`
- `IGame.GetValidCommands()` internally delegates to current phase
- Each phase can have its own `GetValidCommands()` implementation

### Command Parameterization and Configuration

**Contract:** `GetValidCommands()` returns **exactly one instance of each valid command type** at the current moment.

- **Non-parameterized commands** (e.g., `EndTurnCommand`, `RollDiceCommand`) are ready to execute immediately
- **Parameterized commands** (e.g., `BuildRoadCommand`, `OfferTradeCommand`) require configuration before execution
- Commands are implemented as classes with mutable properties for configuration
- `CanExecute()` validates that required parameters are properly configured

**Helper Methods:**
Phases may expose helper methods to assist with configuring parameterized commands:
```csharp
public interface IPostRollPhase
{
    IEnumerable<IPostRollPhaseCommand> GetValidCommands();
    
    // Helpers for parameterized commands (optional, for client convenience)
    IEnumerable<EdgeCoordinate> GetValidRoadPlacements();
    IEnumerable<IntersectionCoordinate> GetValidSettlementPlacements();
}
```

Helpers are provided when the parameter space is:
- Finite and practical to enumerate (e.g., valid road placements)
- Requires complex game logic that shouldn't be duplicated by clients

Helpers are **not** provided when:
- The parameter space is infinite or impractically large (e.g., all possible trade offers)
- Smart clients can reasonably implement the logic themselves

### Command Discrimination via Visitor Pattern

Commands use the same visitor-based discrimination as phases:

**Phase-Specific Command Hierarchies:**
- Leaf phases (phases with no subphases) expose `GetValidCommands()` returning phase-specific command types
- Each leaf phase defines its own command interface (e.g., `IPostRollPhaseCommand`, `IPlaceSettlementPhaseCommand`)
- Phase-specific command interfaces define their own visitor interface
- Concrete commands implement `Accept()` for each phase-specific visitor

**Example:**

```csharp
// Leaf phase exposes phase-specific commands
public interface IPostRollPhase : ITurnSubPhase
{
    IEnumerable<IPostRollPhaseCommand> GetValidCommands();
}

// Phase-specific command interface with visitor
public interface IPostRollPhaseCommand : IGameCommand 
{
    void Accept(IPostRollPhaseCommandVisitor visitor);
}

public interface IPostRollPhaseCommandVisitor
{
    void Visit(BuildRoadCommand command);
    void Visit(BuildSettlementCommand command);
    void Visit(BuyDevelopmentCardCommand command);
    void Visit(PlayKnightCardCommand command);
    void Visit(MaritimeTradeCommand command);
    void Visit(EndTurnCommand command);
}

// Concrete command implements visitor for each phase it appears in
public class BuildRoadCommand : IGameCommand, IPostRollPhaseCommand, IPlaceRoadPhaseCommand
{
    public EdgeCoordinate Location { get; set; }
    
    public void Execute(IGame game) { /* ... */ }
    public bool CanExecute(IGame game) { /* ... */ }
    
    // Implement Accept for each phase-specific visitor
    public void Accept(IPostRollPhaseCommandVisitor visitor) => visitor.Visit(this);
    public void Accept(IPlaceRoadPhaseCommandVisitor visitor) => visitor.Visit(this);
}
```

**Client Usage:**

```csharp
// Exhaustive handling with compile-time safety using visitor
public class PostRollUI : IPostRollPhaseCommandVisitor
{
    private IPostRollPhase _phase;
    
    public void RenderActions()
    {
        foreach (var command in _phase.GetValidCommands())
        {
            command.Accept(this); // Exhaustively handles all post-roll commands
        }
    }
    
    public void Visit(BuildRoadCommand command) 
    { 
        // Command needs configuration - use helper to get valid placements
        foreach (var location in _phase.GetValidRoadPlacements())
        {
            command.Location = location;
            // Render button for this specific placement
        }
    }
    
    public void Visit(OfferTradeCommand command) 
    { 
        // No helper available - show dialog for client to configure trade
        // Render "Offer Trade..." button that opens trade configuration UI
    }
    
    public void Visit(EndTurnCommand command) 
    { 
        // No configuration needed - ready to execute
        // Render "End Turn" button
    }
    
    public void Visit(BuyDevelopmentCardCommand command) { /* ... */ }
    // ... must implement all methods
}

// Ad-hoc handling when exhaustiveness not needed
foreach (var command in postRollPhase.GetValidCommands())
{
    if (command is BuildRoadCommand buildRoad)
    {
        // Configure using helper
        foreach (var location in postRollPhase.GetValidRoadPlacements())
        {
            buildRoad.Location = location;
            // Use configured command
        }
    }
    else if (command is EndTurnCommand endTurn)
    {
        // Execute directly - no configuration needed
        game.Execute(endTurn);
    }
}
```

**Benefits:**
- **Compile-time exhaustiveness**: Must handle all commands valid in a phase
- **Type safety**: Phase interfaces document exactly what command types are valid
- **Self-documenting**: Command implements phase-specific interfaces to show where it's valid
- **Organized client code**: Visitor pattern encourages structured command handling per phase

**Tradeoffs:**
- More verbose: Requires phase-specific command interface + visitor for each leaf phase
- Commands valid in multiple phases implement multiple `Accept()` methods
