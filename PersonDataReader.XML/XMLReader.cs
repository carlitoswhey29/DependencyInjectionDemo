using PeopleViewer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace PersonDataReader.XML
{
    public class XMLReader : IPersonReader
    {
        public IXMLFileLoader FileLoader { get; set; }

        public XMLReader()
        {
            string filePath =
                AppDomain.CurrentDomain.BaseDirectory + "People.xml";
            FileLoader = new XMLFileLoader(filePath);
        }

        public IEnumerable<Person> GetPeople()
        {
            var fileData = FileLoader.LoadFile();
            var people = ParseDataString(fileData);
            return people;
        }

        public Person GetPerson(int id)
        {
            var shapes = GetPeople();
            return shapes?.FirstOrDefault(s => s.Id == id);
        }

        private IEnumerable<Person> ParseDataString(XElement xmlData)
        {
            var shapes = new List<Person>();
            var xmlShapes = xmlData.Elements();

            IEnumerable<string> lines = xmlShapes.Elements()
                        .Select(person => person.Value)
                        .Where(p => !string.IsNullOrWhiteSpace(p)).ToList();

            foreach (string line in lines)
            {
                try
                {
                    shapes.Add(ParsePersonString(line));
                }
                catch (Exception)
                {
                    // Skip the bad record, log it, and move to the next record
                    // log.Write($"Unable to parse record: {line}")
                }
            }
            return shapes;
        }

        private Person ParsePersonString(string personData)
        {
            personData = personData.TrimStart();
            var elements = personData.Split('\n');
            var shape = new Person()
            {
                Id = int.Parse(elements[0]),
                GivenName = elements[1].Trim(),
                FamilyName = elements[2].Trim(),
                StartDate = DateTime.Parse(elements[3]),
                Rating = int.Parse(elements[4]),
                FormatString = elements[5].Trim(),
            };
            return shape;
        }
    }
}

