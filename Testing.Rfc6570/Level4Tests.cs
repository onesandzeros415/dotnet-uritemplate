﻿namespace Testing.Rfc6570
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Rackspace.Net;

    [TestClass]
    public class Level4Tests
    {
        private static readonly IDictionary<string, object> variables =
            new Dictionary<string, object>
            {
                { "var", "value" },
                { "hello", "Hello World!" },
                { "path", "/foo/bar" },
                { "list", new[] { "red", "green", "blue" } },
                { "keys", new Dictionary<string, string> { { "semi", ";" }, { "dot", "." }, { "comma", ","} } }
            };

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.SimpleExpansion)]
        public void TestSimpleExpansionPrefix()
        {
            string template = "{var:3}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            Assert.AreEqual("val", uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.SimpleExpansion)]
        public void TestSimpleExpansionLongPrefix()
        {
            string template = "{var:30}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            Assert.AreEqual("value", uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.SimpleExpansion)]
        public void TestSimpleExpansionCollectionVariable()
        {
            string template = "{list}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            Assert.AreEqual("red,green,blue", uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.SimpleExpansion)]
        public void TestCompoundSimpleExpansionCollectionVariable()
        {
            string template = "{list*}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            Assert.AreEqual("red,green,blue", uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.SimpleExpansion)]
        public void TestSimpleExpansionAssociativeMapVariable()
        {
            string template = "{keys}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            string[] allowed =
                {
                    "comma,%2C,dot,.,semi,%3B",
                    "comma,%2C,semi,%3B,dot,.",
                    "dot,.,comma,%2C,semi,%3B",
                    "dot,.,semi,%3B,comma,%2C",
                    "semi,%3B,comma,%2C,dot,.",
                    "semi,%3B,dot,.,comma,%2C"
                };

            CollectionAssert.Contains(allowed, uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.SimpleExpansion)]
        public void TestCompoundSimpleExpansionAssociativeMapVariable()
        {
            string template = "{keys*}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            string[] allowed =
                {
                    "comma=%2C,dot=.,semi=%3B",
                    "comma=%2C,semi=%3B,dot=.",
                    "dot=.,comma=%2C,semi=%3B",
                    "dot=.,semi=%3B,comma=%2C",
                    "semi=%3B,comma=%2C,dot=.",
                    "semi=%3B,dot=.,comma=%2C"
                };

            CollectionAssert.Contains(allowed, uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.ReservedExpansion)]
        public void TestReservedExpansionPrefixVariable()
        {
            string template = "{+path:6}/here";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            Assert.AreEqual("/foo/b/here", uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.ReservedExpansion)]
        public void TestReservedExpansionCollectionVariable()
        {
            string template = "{+list}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            Assert.AreEqual("red,green,blue", uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.ReservedExpansion)]
        public void TestCompoundReservedExpansionCollectionVariable()
        {
            string template = "{+list*}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            Assert.AreEqual("red,green,blue", uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.ReservedExpansion)]
        public void TestReservedExpansionAssociativeMapVariable()
        {
            string template = "{+keys}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            string[] allowed =
                {
                    "comma,,,dot,.,semi,;",
                    "comma,,,semi,;,dot,.",
                    "dot,.,comma,,,semi,;",
                    "dot,.,semi,;,comma,,",
                    "semi,;,comma,,,dot,.",
                    "semi,;,dot,.,comma,,"
                };

            CollectionAssert.Contains(allowed, uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.ReservedExpansion)]
        public void TestCompoundReservedExpansionAssociativeMapVariable()
        {
            string template = "{+keys*}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            string[] allowed =
                {
                    "comma=,,dot=.,semi=;",
                    "comma=,,semi=;,dot=.",
                    "dot=.,comma=,,semi=;",
                    "dot=.,semi=;,comma=,",
                    "semi=;,comma=,,dot=.",
                    "semi=;,dot=.,comma=,"
                };

            CollectionAssert.Contains(allowed, uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.FragmentExpansion)]
        public void TestFragmentExpansionPrefixVariable()
        {
            string template = "{#path:6}/here";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            Assert.AreEqual("#/foo/b/here", uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.FragmentExpansion)]
        public void TestFragmentExpansionCollectionVariable()
        {
            string template = "{#list}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            Assert.AreEqual("#red,green,blue", uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.FragmentExpansion)]
        public void TestCompoundFragmentExpansionCollectionVariable()
        {
            string template = "{#list*}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            Assert.AreEqual("#red,green,blue", uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.FragmentExpansion)]
        public void TestFragmentExpansionAssociativeMapVariable()
        {
            string template = "{#keys}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            string[] allowed =
                {
                    "#comma,,,dot,.,semi,;",
                    "#comma,,,semi,;,dot,.",
                    "#dot,.,comma,,,semi,;",
                    "#dot,.,semi,;,comma,,",
                    "#semi,;,comma,,,dot,.",
                    "#semi,;,dot,.,comma,,"
                };

            CollectionAssert.Contains(allowed, uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.FragmentExpansion)]
        public void TestCompoundFragmentExpansionAssociativeMapVariable()
        {
            string template = "{#keys*}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            string[] allowed =
                {
                    "#comma=,,dot=.,semi=;",
                    "#comma=,,semi=;,dot=.",
                    "#dot=.,comma=,,semi=;",
                    "#dot=.,semi=;,comma=,",
                    "#semi=;,comma=,,dot=.",
                    "#semi=;,dot=.,comma=,"
                };

            CollectionAssert.Contains(allowed, uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.LabelExpansion)]
        public void TestLabelExpansionPrefix()
        {
            string template = "X{.var:3}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            Assert.AreEqual("X.val", uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.LabelExpansion)]
        public void TestLabelExpansionCollectionVariable()
        {
            string template = "X{.list}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            Assert.AreEqual("X.red,green,blue", uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.LabelExpansion)]
        public void TestCompoundLabelExpansionCollectionVariable()
        {
            string template = "X{.list*}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            Assert.AreEqual("X.red.green.blue", uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.LabelExpansion)]
        public void TestLabelExpansionAssociativeMapVariable()
        {
            string template = "X{.keys}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            string[] allowed =
                {
                    "X.comma,%2C,dot,.,semi,%3B",
                    "X.comma,%2C,semi,%3B,dot,.",
                    "X.dot,.,comma,%2C,semi,%3B",
                    "X.dot,.,semi,%3B,comma,%2C",
                    "X.semi,%3B,comma,%2C,dot,.",
                    "X.semi,%3B,dot,.,comma,%2C"
                };

            CollectionAssert.Contains(allowed, uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.PathSegmentExpansion)]
        public void TestPathSegmentExpansionMultipleReferencesPrefix()
        {
            string template = "{/var:1,var}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            Assert.AreEqual("/v/value", uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.PathSegmentExpansion)]
        public void TestPathSegmentExpansionCollectionVariable()
        {
            string template = "{/list}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            Assert.AreEqual("/red,green,blue", uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.PathSegmentExpansion)]
        public void TestCompoundPathSegmentExpansionCollectionVariable()
        {
            string template = "{/list*}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            Assert.AreEqual("/red/green/blue", uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.PathSegmentExpansion)]
        public void TestCompoundPathSegmentExpansionCollectionVariableAndPrefixVariableReference()
        {
            string template = "{/list*,path:4}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            Assert.AreEqual("/red/green/blue/%2Ffoo", uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.PathSegmentExpansion)]
        public void TestPathSegmentExpansionAssociativeMapVariable()
        {
            string template = "{/keys}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            string[] allowed =
                {
                    "/comma,%2C,dot,.,semi,%3B",
                    "/comma,%2C,semi,%3B,dot,.",
                    "/dot,.,comma,%2C,semi,%3B",
                    "/dot,.,semi,%3B,comma,%2C",
                    "/semi,%3B,comma,%2C,dot,.",
                    "/semi,%3B,dot,.,comma,%2C"
                };

            CollectionAssert.Contains(allowed, uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.PathSegmentExpansion)]
        public void TestCompoundPathSegmentExpansionAssociativeMapVariable()
        {
            string template = "{/keys*}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            string[] allowed =
                {
                    "/comma=%2C/dot=./semi=%3B",
                    "/comma=%2C/semi=%3B/dot=.",
                    "/dot=./comma=%2C/semi=%3B",
                    "/dot=./semi=%3B/comma=%2C",
                    "/semi=%3B/comma=%2C/dot=.",
                    "/semi=%3B/dot=./comma=%2C"
                };

            CollectionAssert.Contains(allowed, uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.PathParameterExpansion)]
        public void TestPathParameterExpansionPrefixVariable()
        {
            string template = "{;hello:5}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            Assert.AreEqual(";hello=Hello", uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.PathParameterExpansion)]
        public void TestPathParameterExpansionCollectionVariable()
        {
            string template = "{;list}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            Assert.AreEqual(";list=red,green,blue", uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.PathParameterExpansion)]
        public void TestCompoundPathParameterExpansionCollectionVariable()
        {
            string template = "{;list*}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            Assert.AreEqual(";list=red;list=green;list=blue", uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.PathParameterExpansion)]
        public void TestPathParameterExpansionAssociativeMapVariable()
        {
            string template = "{;keys}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            string[] allowed =
                {
                    ";keys=comma,%2C,dot,.,semi,%3B",
                    ";keys=comma,%2C,semi,%3B,dot,.",
                    ";keys=dot,.,comma,%2C,semi,%3B",
                    ";keys=dot,.,semi,%3B,comma,%2C",
                    ";keys=semi,%3B,comma,%2C,dot,.",
                    ";keys=semi,%3B,dot,.,comma,%2C"
                };

            CollectionAssert.Contains(allowed, uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.PathParameterExpansion)]
        public void TestCompoundPathParameterExpansionAssociativeMapVariable()
        {
            string template = "{;keys*}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            string[] allowed =
                {
                    ";comma=%2C;dot=.;semi=%3B",
                    ";comma=%2C;semi=%3B;dot=.",
                    ";dot=.;comma=%2C;semi=%3B",
                    ";dot=.;semi=%3B;comma=%2C",
                    ";semi=%3B;comma=%2C;dot=.",
                    ";semi=%3B;dot=.;comma=%2C"
                };

            CollectionAssert.Contains(allowed, uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.QueryExpansion)]
        public void TestQueryExpansionPrefixVariable()
        {
            string template = "{?var:3}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            Assert.AreEqual("?var=val", uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.QueryExpansion)]
        public void TestQueryExpansionCollectionVariable()
        {
            string template = "{?list}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            Assert.AreEqual("?list=red,green,blue", uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.QueryExpansion)]
        public void TestCompoundQueryExpansionCollectionVariable()
        {
            string template = "{?list*}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            Assert.AreEqual("?list=red&list=green&list=blue", uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.QueryExpansion)]
        public void TestQueryExpansionAssociativeMapVariable()
        {
            string template = "{?keys}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            string[] allowed =
                {
                    "?keys=comma,%2C,dot,.,semi,%3B",
                    "?keys=comma,%2C,semi,%3B,dot,.",
                    "?keys=dot,.,comma,%2C,semi,%3B",
                    "?keys=dot,.,semi,%3B,comma,%2C",
                    "?keys=semi,%3B,comma,%2C,dot,.",
                    "?keys=semi,%3B,dot,.,comma,%2C"
                };

            CollectionAssert.Contains(allowed, uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.QueryExpansion)]
        public void TestCompoundQueryExpansionAssociativeMapVariable()
        {
            string template = "{?keys*}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            string[] allowed =
                {
                    "?comma=%2C&dot=.&semi=%3B",
                    "?comma=%2C&semi=%3B&dot=.",
                    "?dot=.&comma=%2C&semi=%3B",
                    "?dot=.&semi=%3B&comma=%2C",
                    "?semi=%3B&comma=%2C&dot=.",
                    "?semi=%3B&dot=.&comma=%2C"
                };

            CollectionAssert.Contains(allowed, uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.QueryContinuationExpansion)]
        public void TestQueryContinuationExpansionPrefixVariable()
        {
            string template = "{&var:3}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            Assert.AreEqual("&var=val", uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.QueryContinuationExpansion)]
        public void TestQueryContinuationExpansionCollectionVariable()
        {
            string template = "{&list}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            Assert.AreEqual("&list=red,green,blue", uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.QueryContinuationExpansion)]
        public void TestCompoundQueryContinuationExpansionCollectionVariable()
        {
            string template = "{&list*}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            Assert.AreEqual("&list=red&list=green&list=blue", uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.QueryContinuationExpansion)]
        public void TestQueryContinuationExpansionAssociativeMapVariable()
        {
            string template = "{&keys}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            string[] allowed =
                {
                    "&keys=comma,%2C,dot,.,semi,%3B",
                    "&keys=comma,%2C,semi,%3B,dot,.",
                    "&keys=dot,.,comma,%2C,semi,%3B",
                    "&keys=dot,.,semi,%3B,comma,%2C",
                    "&keys=semi,%3B,comma,%2C,dot,.",
                    "&keys=semi,%3B,dot,.,comma,%2C"
                };

            CollectionAssert.Contains(allowed, uri.ToString());
        }

        [TestMethod]
        [TestCategory(TestCategories.Level4)]
        [TestCategory(TestCategories.QueryContinuationExpansion)]
        public void TestCompoundQueryContinuationExpansionAssociativeMapVariable()
        {
            string template = "{&keys*}";
            UriTemplate uriTemplate = new UriTemplate(template);
            Uri uri = uriTemplate.BindByName(variables);
            string[] allowed =
                {
                    "&comma=%2C&dot=.&semi=%3B",
                    "&comma=%2C&semi=%3B&dot=.",
                    "&dot=.&comma=%2C&semi=%3B",
                    "&dot=.&semi=%3B&comma=%2C",
                    "&semi=%3B&comma=%2C&dot=.",
                    "&semi=%3B&dot=.&comma=%2C"
                };

            CollectionAssert.Contains(allowed, uri.ToString());
        }
    }
}
