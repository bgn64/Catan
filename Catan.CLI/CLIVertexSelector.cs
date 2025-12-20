using Catan.Core;

namespace Catan.CLI;

public class CLIVertexSelector : IVertexProvider, IVertexToStringConverter
{
    IVertexToStringConverter _vertexToStringConverter;
    BoardToStringConverter _boardToStringConverter;
    List<FlatTopCoordinate> _vertices;

    public CLIVertexSelector(BoardToStringConverter boardToStringConverter)
    {
        _vertexToStringConverter = boardToStringConverter.VertexToStringConverter;
        _boardToStringConverter = boardToStringConverter.WithVertexToStringConverter(this);
        _vertices = new List<FlatTopCoordinate>();
    }

    public FlatTopCoordinate GetVertex(Board board)
    {
        _vertices = board.GetHexVertexCoordinates().ToList();

        Console.WriteLine(_boardToStringConverter.ToString(board, 3, 9));
        Console.WriteLine($"Please select a vertex index or 'n' to cycle through vertices: ");

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

                Console.WriteLine(_boardToStringConverter.ToString(board, 3, 9));
                Console.WriteLine($"Please select a vertex index or 'n' to cycle through vertices: ");
                input = Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Please select a valid vertex index or 'n' to cycle through vertices: ");
                input = Console.ReadLine();
            }
        }

        return _vertices[index];
    }

    public string ToString(FlatTopCoordinate vertex, Board board)
    {
        int vertexIndex = _vertices.IndexOf(vertex);

        if (0 <= vertexIndex && vertexIndex < 10)
        {
            return $"{vertexIndex}";
        }
        else 
        {
            return _vertexToStringConverter.ToString(vertex, board);
        }
    }
}
