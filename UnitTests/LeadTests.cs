using Microsoft.VisualStudio.TestTools.UnitTesting;
using QbModels.ENUM;

namespace QbModels.Tests
{
    [TestClass]
    public class LeadTests
    {
        [TestMethod]
        public void TestLeadQueryRq()
        {
            LeadQueryRq leadRq = new();
            Assert.IsTrue(leadRq.IsEntityValid());

            leadRq.ListID = new() { "JobTypeQueryRq.ListID" };
            leadRq.MaxReturned = -1;
            Assert.IsTrue(leadRq.IsEntityValid());

            leadRq.ListID = null;
            leadRq.FullName = new() { "JobTypeQueryRq.FullName" };
            Assert.IsTrue(leadRq.IsEntityValid());

            leadRq.FullName = null;
            leadRq.NameFilter = new();
            leadRq.MaxReturned = 99999;
            Assert.IsFalse(leadRq.IsEntityValid());

            leadRq.NameFilter.MatchCriterion = MatchCriterion.None;
            leadRq.NameFilter.Name = "A";
            Assert.IsFalse(leadRq.IsEntityValid());

            leadRq.NameFilter.MatchCriterion = MatchCriterion.Contains;
            Assert.IsTrue(leadRq.IsEntityValid());

            leadRq.NameRangeFilter = new();
            leadRq.NameRangeFilter.FromName = "A";
            leadRq.NameRangeFilter.ToName = "ZZ";
            Assert.IsFalse(leadRq.IsEntityValid());

            leadRq.NameFilter = null;
            Assert.IsTrue(leadRq.IsEntityValid());

            var model = new QryRqModel<LeadQueryRq>();
            model.SetRequest(leadRq, "QryRq");
            Assert.IsTrue(leadRq.ToString().Contains("<LeadQueryRq>"));
            Assert.IsTrue(model.ToString().Contains("<LeadQueryRq>"));
        }

        [TestMethod]
        public void TestLeadAddRq()
        {
            LeadAddRq leadRq = new() { CompanyName = "Some Company" };
            Assert.IsFalse(leadRq.IsEntityValid());

            leadRq.FullName = "LeadAddRq.FullName";
            leadRq.Status = LeadStatus.None;
            Assert.IsFalse(leadRq.IsEntityValid());

            leadRq.Status = LeadStatus.Warm;
            Assert.IsTrue(leadRq.IsEntityValid());

            leadRq.AdditionalContact = new();
            leadRq.AdditionalContact.Add(new());
            Assert.IsFalse(leadRq.IsEntityValid());

            leadRq.Locations = new();
            leadRq.Locations.Add(new() { Location = "Main site" });
            leadRq.Locations.Add(new() { Location = "Off site", LeadAddress = new() { Addr1 = "123 Main St", City = "Anywhere", Country = "USA" } });
            leadRq.Locations.Add(new() { Location = "Disaster recovery site" });
            Assert.IsFalse(leadRq.IsEntityValid());

            leadRq.AdditionalContact[0] = new() { ContactName = "AdditionalContact.ContactName", ContactValue = "AdditionalContact.ContactValue" };
            leadRq.AdditionalContact.Add(new() { ContactName = "AdditionalContact.ContactName #2", ContactValue = "AdditionalContact.ContactValue #2" });
            Assert.IsTrue(leadRq.IsEntityValid());

            leadRq.LeadContacts = new();
            Assert.IsTrue(leadRq.IsEntityValid());

            leadRq.LeadContacts.Add(new());
            leadRq.LeadContacts[0].AdditionalContact = new() 
            { 
                new() { ContactName = "Contact 1", ContactValue = "Value 1" },
                new() { ContactName = "Contact 2", ContactValue = "Value 2" },
                new() { ContactName = "Contact 3", ContactValue = "Value 3" },
                new() { ContactName = "Contact 4", ContactValue = "Value 4" },
                new() { ContactName = "Contact 5", ContactValue = "Value 5" },
                new() { ContactName = "Contact 6", ContactValue = "Value 6" }
            };
            Assert.IsFalse(leadRq.IsEntityValid());

            leadRq.LeadContacts[0].FirstName = "LeadContacts.FirstName";
            leadRq.LeadContacts[0].LastName = "LeadContactsDto.LastName";
            leadRq.LeadContacts[0].IsPrimaryContact = true;
            leadRq.LeadContacts[0].JobTitle = "Main Lead";
            leadRq.LeadContacts[0].AdditionalContact.Clear();
            leadRq.LeadContacts[0].AdditionalContact.Add(new() { ContactName = "AdditionalContact.ContactName", ContactValue = "AdditionalContact.ContactValue" });
            leadRq.LeadContacts[0].AdditionalContact.Add(new() { ContactName = "ContactName 2", ContactValue = "ContactValue 2" });
            leadRq.LeadContacts[0].AdditionalContact.Add(new() { ContactName = "ContactName 3", ContactValue = "ContactValue 3" });
            leadRq.LeadContacts[0].AdditionalContact.Add(new() { ContactName = "ContactName 4", ContactValue = "ContactValue 4" });
            leadRq.LeadContacts[0].AdditionalContact.Add(new() { ContactName = "ContactName 5", ContactValue = "ContactValue 5" });
            Assert.IsTrue(leadRq.IsEntityValid());

            var model = new AddRqModel<LeadAddRq>("LeadAdd");
            model.SetRequest(leadRq, "AddRq");
            Assert.IsTrue(leadRq.ToString().Contains("<LeadAddRq>"));
            Assert.IsTrue(model.ToString().Contains("<LeadAddRq>"));
        }

        [TestMethod]
        public void TestLeadModRq()
        {
            LeadModRq leadRq = new();
            Assert.IsFalse(leadRq.IsEntityValid());

            leadRq.ListID = "LeadAddRq.ListID";
            leadRq.EditSequence = "LeadAddRq.EditSequence";
            leadRq.FullName = "LeadAddRq.FullName";
            leadRq.Status = LeadStatus.None;
            Assert.IsFalse(leadRq.IsEntityValid());

            leadRq.Status = LeadStatus.Hot;
            Assert.IsTrue(leadRq.IsEntityValid());

            leadRq.AdditionalContact = new();
            leadRq.AdditionalContact.Add(new());
            Assert.IsFalse(leadRq.IsEntityValid());

            leadRq.AdditionalContact[0].ContactName = "AdditionalContact.ContactName";
            leadRq.AdditionalContact[0].ContactValue = "AdditionalContact.ContactValue";
            Assert.IsTrue(leadRq.IsEntityValid());

            leadRq.LeadContacts = new();
            Assert.IsTrue(leadRq.IsEntityValid());

            leadRq.LeadContacts.Add(new());
            leadRq.LeadContacts[0].AdditionalContact = new() { new(), new(), new(), new(), new(), new() };
            Assert.IsFalse(leadRq.IsEntityValid());

            leadRq.LeadContacts[0].LocationContactID = 123;
            leadRq.LeadContacts[0].FirstName = "LeadContacts.FirstName";
            leadRq.LeadContacts[0].AdditionalContact.Clear();
            leadRq.LeadContacts[0].AdditionalContact.Add(new() { ContactName = "AdditionalContact.ContactName", ContactValue = "AdditionalContact.ContactValue" });
            Assert.IsTrue(leadRq.IsEntityValid());

            var model = new ModRqModel<LeadModRq>("LeadMod");
            model.SetRequest(leadRq, "ModRq");
            Assert.IsTrue(leadRq.ToString().Contains("<LeadModRq>"));
            Assert.IsTrue(model.ToString().Contains("<LeadModRq>"));
        }
    }
}
