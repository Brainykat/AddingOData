using Microsoft.OData.Edm;

namespace AddingOData.Extensions
{
  public interface IODataModelProvider
  {
    IEdmModel GetEdmModel(string apiVersion);
  }
}
