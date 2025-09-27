using ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace ECommerce.Application.Services;

public class UrlBuilderService : IUrlBuilderService
{
    public UrlBuilderService()
    {
    }

    public ILinkBuilder CreateLink(string baseUrl)
    {
        return new LinkBuilder(baseUrl);
    }

    public string DecodeToken(string encodedToken)
    {
        byte[] tokenBytes = WebEncoders.Base64UrlDecode(encodedToken);
        return Encoding.UTF8.GetString(tokenBytes);
    }
}

public class LinkBuilder : ILinkBuilder
{
    private string _link;

    public LinkBuilder(string baseUrl)
    {
        _link = baseUrl;
    }

    private string EncodeToken(string token)
    {
        return WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
    }

    public ILinkBuilder AddParameter(string name, string value)
    {
        _link = QueryHelpers.AddQueryString(_link, name, value);
        return this;
    }

    public ILinkBuilder AddEncodedToken(string name, string tokenValue)
    {
        var encodedToken = EncodeToken(tokenValue);
        _link = QueryHelpers.AddQueryString(_link, name, encodedToken);
        return this;
    }

    public string Build()
    {
        return _link;
    }
}