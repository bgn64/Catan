using Catan.Core;
using System.Text;

namespace Catan.CLI;

public interface IVertexToStringConverter
{
  string ToString(FlatTopCoordinate vertex);
}
