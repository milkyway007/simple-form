using Domain;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedDataAsync(DataContext context)
        {
            if (!context.SectorOptions.Any())
            {
                var sectorOptions = new List<SectorOption>
                {
                    new SectorOption
                    {
                        Label = "Manufacturing",
                        Level = 1,
                        Children =  new List<SectorOption>
                        {
                            new SectorOption
                            {
                                Label = "Construction materials",
                                Level = 2,
                            },
                            new SectorOption
                            {
                                Label = "Electronics and Optics",
                                Level = 2,
                            },
                            new SectorOption
                            {
                                Label = "Food and Beverage",
                                Level = 2,
                                Children = new List<SectorOption>
                                {
                                    new SectorOption
                                    {
                                        Label = "Bakery & confectionery products",
                                        Level = 3,
                                    },
                                    new SectorOption
                                    {
                                        Label = "Fish & fish products",
                                        Level = 3,
                                    },
                                    new SectorOption
                                    {
                                        Label = "Meat & meat products",
                                        Level = 3,
                                    },
                                    new SectorOption
                                    {
                                        Label = "Milk & dairy products",
                                        Level = 3,
                                    },
                                    new SectorOption
                                    {
                                        Label = "Sweets & snack food",
                                        Level = 3,
                                    },
                                    new SectorOption
                                    {
                                        Label = "Other",
                                        Level = 3,
                                    },
                                },
                            },
                            new SectorOption
                            {
                                Label = "Furniture",
                                Level = 2,
                                Children = new List<SectorOption>
                                {
                                    new SectorOption
                                    {
                                        Label = "Bathroom/sauna",
                                        Level = 3,
                                    },
                                    new SectorOption
                                    {
                                        Label = "Bedroom",
                                        Level = 3,
                                    },
                                    new SectorOption
                                    {
                                        Label = "Children’s room",
                                        Level = 3,
                                    },
                                    new SectorOption
                                    {
                                        Label = "Kitchen",
                                        Level = 3,
                                    },
                                    new SectorOption
                                    {
                                        Label = "Living room",
                                        Level = 3,
                                    },
                                    new SectorOption
                                    {
                                        Label = "Office",
                                        Level = 3,
                                    },
                                    new SectorOption
                                    {
                                        Label = "Outdoor",
                                        Level = 3,
                                    },
                                    new SectorOption
                                    {
                                        Label = "Project furniture",
                                        Level = 3,
                                    },
                                    new SectorOption
                                    {
                                        Label = "Other",
                                        Level = 3,
                                    },
                                },
                            },
                            new SectorOption{
                                Label = "Machinery",
                                Level = 2,
                                Children = new List<SectorOption>
                                {
                                    new SectorOption
                                    {
                                        Label = "Machinery components",
                                        Level = 3,
                                    },
                                    new SectorOption
                                    {
                                        Label = "Machinery equipment/tools",
                                        Level = 3,
                                    },
                                    new SectorOption
                                    {
                                        Label = "Manufacture of machinery",
                                        Level = 3,
                                    },
                                    new SectorOption
                                    {
                                        Label = "Maritime",
                                        Level = 3,
                                        Children = new List<SectorOption>
                                        {
                                            new SectorOption
                                            {
                                                Label = "Aluminium and steel workboats",
                                                Level = 4,
                                            },
                                            new SectorOption
                                            {
                                                Label = "Boat/Yacht building",
                                                Level = 4,
                                            },
                                            new SectorOption
                                            {
                                                Label = "Ship repair and conversion",
                                                Level = 4,
                                            },
                                            new SectorOption
                                            {
                                                Label = "Metal structures",
                                                Level = 4,
                                            },
                                            new SectorOption
                                            {
                                                Label = "Repair and maintenance service",
                                                Level = 4,
                                            },
                                            new SectorOption
                                            {
                                                Label = "Other",
                                                Level = 4,
                                            },
                                        },
                                    },
                                }
                            },
                            new SectorOption
                            {
                                Label = "Metalworking",
                                Level = 2,
                                Children = new List<SectorOption>
                                {
                                    new SectorOption
                                    {
                                        Label = "Construction of metal structures",
                                        Level = 3,
                                    },
                                    new SectorOption
                                    {
                                        Label = "Houses and buildings",
                                        Level = 3,
                                    },
                                    new SectorOption
                                    {
                                        Label = "Metal products",
                                        Level = 3,
                                    },
                                    new SectorOption
                                    {
                                        Label = "Metal works",
                                        Level = 3,
                                        Children = new List<SectorOption>
                                        {
                                            new SectorOption
                                            {
                                                Label = "CNC-machining",
                                                Level = 4,
                                            },
                                            new SectorOption
                                            {
                                                Label = "Forgings, Fasteners",
                                                Level = 4,
                                            },
                                            new SectorOption
                                            {
                                                Label = "MIG, TIG, Aluminum welding",
                                                Level = 4,
                                            },
                                            new SectorOption
                                            {
                                                Label = "Plasma, Laser cutting",
                                                Level = 4,
                                            },
                                        }
                                    },
                                }
                            },
                            new SectorOption
                            {
                                Label = "Plastic and Rubber",
                                Level = 2,
                                Children = new List<SectorOption>
                                {
                                    new SectorOption
                                    {
                                        Label = "Packaging",
                                        Level = 3,
                                    },
                                    new SectorOption
                                    {
                                        Label = "Plastic goods",
                                        Level = 3,
                                    },
                                    new SectorOption
                                    {
                                        Label = "Plastic processing technology",
                                        Level = 3,
                                        Children = new List<SectorOption>
                                        {
                                            new SectorOption
                                            {
                                                Label = "Blowing",
                                                Level = 4,
                                            },
                                            new SectorOption
                                            {
                                                Label = "Moulding",
                                                Level = 4,
                                            },
                                            new SectorOption
                                            {
                                                Label = "Plastics welding and processing",
                                                Level = 4,
                                            },
                                            new SectorOption
                                            {
                                                Label = "Plastic profiles",
                                                Level = 4,
                                            },
                                        }
                                    },
                                }
                            },
                            new SectorOption{
                                Label = "Printing",
                                Level = 2,
                                Children = new List<SectorOption>
                                {
                                    new SectorOption
                                    {
                                        Label = "Advertising",
                                        Level = 3,
                                    },
                                    new SectorOption
                                    {
                                        Label = "Book/Periodicals printing",
                                        Level = 3,
                                    },
                                    new SectorOption
                                    {
                                        Label = "Labelling and packaging printing",
                                        Level = 3,
                                    },
                                },
                            },
                            new SectorOption
                            {
                                Label = "Textile and Clothing",
                                Level = 2,
                                Children = new List<SectorOption>
                                {
                                    new SectorOption
                                    {
                                        Label = "Clothing",
                                        Level = 3,
                                    },
                                    new SectorOption
                                    {
                                        Label = "Textile",
                                        Level = 3,
                                    },
                                },
                            },
                            new SectorOption
                            {
                                Label = "Wood",
                                Level = 2,
                                Children = new List<SectorOption>
                                {
                                    new SectorOption
                                    {
                                        Label = "Wooden building materials",
                                        Level = 3,
                                    },
                                    new SectorOption
                                    {
                                        Label = "Wooden houses",
                                        Level = 3,
                                    },
                                    new SectorOption
                                    {
                                        Label = "Other",
                                        Level = 3,
                                    },
                                },
                            },
                        },
                    },
                    new SectorOption
                    {
                        Label = "Service",
                        Level = 1,
                        Children = new List<SectorOption>
                        {
                            new SectorOption
                            {
                                Label = "Business services",
                                Level = 2,
                            },
                            new SectorOption
                            {
                                Label = "Engineering",
                                Level = 2,
                            },
                            new SectorOption
                            {
                                Label = "Information Technology and Telecommunications",
                                Level = 2,
                                Children = new List<SectorOption>
                                {
                                    new SectorOption
                                    {
                                        Label = "Data processing, Web portals, E-marketing",
                                        Level = 3,
                                    },
                                    new SectorOption
                                    {
                                        Label = "Programming, Consultancy",
                                        Level = 3,
                                    },
                                    new SectorOption
                                    {
                                        Label = "Software, Hardware",
                                        Level = 3,
                                    },
                                    new SectorOption
                                    {
                                        Label = "Telecommunications",
                                        Level = 3,
                                    },
                                }
                            },                            
                            new SectorOption
                            {
                                Label = "Tourism",
                                Level = 2,
                            },
                            new SectorOption
                            {
                                Label = "Translation services",
                                Level = 2,
                            },
                            new SectorOption
                            {
                                Label = "Transport and Logistics",
                                Level = 2,
                                Children = new List<SectorOption>
                                {
                                    new SectorOption
                                    {
                                        Label = "Air",
                                        Level = 3,
                                    },
                                    new SectorOption
                                    {
                                        Label = "Rail",
                                        Level = 3,
                                    },
                                    new SectorOption
                                    {
                                        Label = "Road",
                                        Level = 3,
                                    },
                                    new SectorOption
                                    {
                                        Label = "Water",
                                        Level = 3,
                                    },
                                }
                            }
                        },
                    },
                    new SectorOption
                    {
                        Label = "Other",
                        Level = 1,
                        Children = new List<SectorOption>
                        {
                            new SectorOption
                            {
                                Label = "Creative industries",
                                Level = 2,
                            },
                            new SectorOption
                            {
                                Label = "Energy technology",
                                Level = 2,
                            },
                            new SectorOption
                            {
                                Label = "Environment",
                                Level = 2,
                            },
                        },
                    },
                };

                await context.SectorOptions.AddRangeAsync(sectorOptions);
                await context.SaveChangesAsync();
            }
        }
    }
}