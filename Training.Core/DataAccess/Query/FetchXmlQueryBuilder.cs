using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Training.Core.DataAccess.Query
{
    public class FetchXmlQueryBuilder
    {
        private const string EntityNodeName = "entity";
        private const string AttributeNodeName = "attribute";
        private const string AllAttributesNodeName = "all-attributes";

        private readonly XmlDocument query;

        private XmlElement entityNode;

        public FetchXmlQueryBuilder()
        {
            query = new XmlDocument();
        }

        public FetchXmlQueryBuilder(string fetchXml)
        {
            query = new XmlDocument();
            query.LoadXml(fetchXml);
        }

        public FetchXmlQueryBuilder RemovePrimaryEntityAttributes()
        {
            GetEntityNode();

            var attributeNodes = entityNode.SelectNodes("attribute");

            foreach (var attributeNode in attributeNodes)
            {
                entityNode.RemoveChild((XmlNode)attributeNode);
            }

            CheckEntityNodeEmpty();

            return this;
        }

        public FetchXmlQueryBuilder AddAllAttributesToPrimaryEntity()
        {
            GetEntityNode();

            var allAttributesElement = query.CreateElement(AllAttributesNodeName);

            entityNode.PrependChild(allAttributesElement);

            return this;
        }

        public override string ToString()
        {
            GetEntityNode();

            CheckEntityNodeEmpty();

            return query.InnerXml;
        }

        private void GetEntityNode()
        {
            if (entityNode == null)
            {
                entityNode = (XmlElement)query.SelectSingleNode("/fetch/entity");
            }
        }

        private void CheckEntityNodeEmpty()
        {
            if (entityNode.IsEmpty == false && entityNode.ChildNodes.Count == 0)
            {
                entityNode.IsEmpty = true;
            }
        }
    }
}
