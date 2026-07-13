using System.ServiceModel.Syndication;
using System.Xml;

namespace free.Crawler;

public struct RssItem
{
    public TextSyndicationContent Title { get; }
    public SyndicationContent Content { get; }
    public TextSyndicationContent Summary { get; }
    public string Id { get; }
    public DateTimeOffset PublishDate { get; }
    public DateTimeOffset LastUpdatedTime { get; }
    public List<SyndicationLink> Links { get; }
    public List<SyndicationPerson> Authors { get; }
    public List<SyndicationPerson> Contributors { get; }
    public List<SyndicationCategory> Categories { get; }
    public TextSyndicationContent Copyright { get; }
    public SyndicationFeed SourceFeed { get; }
    public Uri BaseUri { get; }
    public SyndicationElementExtensionCollection ElementExtensions { get; }
    public Dictionary<XmlQualifiedName, string> AttributeExtensions { get; }
}