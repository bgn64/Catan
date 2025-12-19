using Catan.Core;

namespace Catan.CLI;

public class CLIEdgeSelector : IEdgeProvider, IVertexToStringConverter
{
    Game _game;
    IVertexToStringConverter _vertexToStringConverter;
    BoardToStringConverter _boardToStringConverter;
    List<FlatTopCoordinate> _vertices;

    public CLIEdgeSelector(Game game, BoardToStringConverter boardToStringConverter)
    {
        _game = game;
        _vertexToStringConverter = boardToStringConverter.VertexToStringConverter;
        _boardToStringConverter = boardToStringConverter.WithVertexToStringConverter(this);
        _vertices = new List<FlatTopCoordinate>();
    }

    public Edge GetEdge()
    {
        FlatTopCoordinate? firstVertex = null;
        FlatTopCoordinate? secondVertex = null;
        Edge? edge = null;

        do
        {
            _vertices = _game.Board.GetHexVertexCoordinates().ToList();

            Console.WriteLine(_boardToStringConverter.ToString(_game, 3, 9));
            Console.WriteLine($"Please select the first vertex index or 'n' to cycle through vertices: ");

            string? input = Console.ReadLine();
            int index = 0;

            while (!int.TryParse(input, out index) || index < 0 || index >= _vertices.Count || index >= 10)
            {
                if (input == "n")
                {
                    for (int i = 0; i < 10; i++)
                    {
                        FlatTopCoordinate coordinate = _vertices[0];
                        _vertices.RemoveAt(0);
                        _vertices.Add(coordinate);
                    }

                    Console.WriteLine(_boardToStringConverter.ToString(_game, 3, 9));
                    Console.WriteLine($"Please select the first vertex index or 'n' to cycle through vertices: ");
                    input = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Please select a valid vertex index or 'n' to cycle through vertices: ");
                    input = Console.ReadLine();
                }
            }

            firstVertex = _vertices[index];
            _vertices.RemoveAt(index);

            Console.WriteLine(_boardToStringConverter.ToString(_game, 3, 9));
            Console.WriteLine($"Please select the second vertex index for the edge or 'n' to cycle through the vertices: ");

            input = Console.ReadLine();

            while (!int.TryParse(input, out index) || index < 0 || index >= _vertices.Count || index >= 10)
            {
                if (input == "n")
                {
                    for (int i = 0; i < 10; i++)
                    {
                        FlatTopCoordinate coordinate = _vertices[0];
                        _vertices.RemoveAt(0);
                        _vertices.Add(coordinate);
                    }

                    Console.WriteLine(_boardToStringConverter.ToString(_game, 3, 9));
                    Console.WriteLine($"Please select the second vertex index or 'n' to cycle through vertices: ");
                    input = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Please select a valid vertex index or 'n' to cycle through vertices: ");
                    input = Console.ReadLine();
                }
            }

            secondVertex = _vertices[index];
        }
        while (!Edge.TryGetEdge(_game.Board, firstVertex, secondVertex, out edge));

        Console.WriteLine($"Selected edge {firstVertex}, {secondVertex}");

        return edge!;
    }

    public string ToString(FlatTopCoordinate vertex)
    {
        int vertexIndex = _vertices.IndexOf(vertex);

        if (0 <= vertexIndex && vertexIndex < 10)
        {
            return $"{vertexIndex}";
        }
        else 
        {
            return _vertexToStringConverter.ToString(vertex);
        } 
    }
}
