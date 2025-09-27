namespace ECommerce.Application.Interfaces;

public interface IUrlBuilderService
{
    ILinkBuilder CreateLink(string baseUrl);
    string DecodeToken(string encodedToken);
}

public interface ILinkBuilder
{
    ILinkBuilder AddParameter(string name, string value);
    ILinkBuilder AddEncodedToken(string name, string tokenValue);
    string Build();
}
