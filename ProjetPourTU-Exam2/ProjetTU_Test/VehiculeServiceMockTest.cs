using NUnit.Framework;
using ProjetPourTU.Model;
using ProjetPourTU.Services;
using System.Collections.Generic;
using ProjetTU_Test.Mock;
using System.ComponentModel.DataAnnotations;
using ProjetPourTU.Services.CustomExceptions;
using System.Linq;

namespace ProjetTU_Test
{
    public class VehiculeServiceMockTest
    {
        VehiculeServiceMock _vs;
        [SetUp]
        public void Setup()
        {
            _vs = new VehiculeServiceMock();

        }
        //Zone Test ADD Vehicule
        
        [Test]
        public void TestAddVehiculeRéussit()
        {
            var AvantAjout = _vs.getAll();
            int n1 = AvantAjout.Count;
            Vehicule searched = new Vehicule();
            searched.ID = 4; searched.Nom = "2002"; searched.Immatriculation = "gsqdf6";
            _vs.AddVehicule(searched);
            var ApresAjout = _vs.getAll();
            int n2 = ApresAjout.Count;
            Assert.AreEqual(n1 + 1, n2);
        }
        [Test]  
        public void TestAddVehiculeSameID()
        {
            try
            {
                Vehicule searched = new Vehicule();
                searched.ID = 1; searched.Nom = "2002"; searched.Immatriculation = "gsqdf6";
                _vs.AddVehicule(searched);
                Assert.Fail("Meme ID");
            }catch(SameIDExistsException e)
            {
            }
 
        }
        [Test]
        //Zone test creer message
        public void TestCreerMessage_PourUnVehiculeRéussit()
        {
            var v = new Vehicule();
            v.Immatriculation = "ffds55";
            v.Nom = "Dacia";
            var re = _vs.CreerMessagePourUnVehicule(v);
            var waited = "Véhicule : Dacia, immatriculation : ffds55";
            Assert.AreEqual(waited, re);
        }
        [Test]
        public void TestCreerMessageRéussit()
        {
            var rep = _vs.CreerMessage();
            var waited = "Véhicule : Bagnole1, immatriculation : IMM1111\nVéhicule : Bagnole2, immatriculation : IMM2222\nVéhicule : Bagnole3, immatriculation : IMM3333";
            Assert.AreEqual(waited, rep);
        }
        //Zone test GetByID
        [Test]
        public void TestGetById_Réussit()
        {
            Vehicule searched = new Vehicule() { ID = 1, Nom = "Bagnole1", Immatriculation = "IMM1111" };
            var resault = _vs.getByID(searched.ID);
            Assert.AreEqual(searched.Immatriculation, resault.Immatriculation);
            Assert.AreEqual(searched.Nom, resault.Nom);
        }
        [TestCase(1, "Bagnole1")]
        [TestCase(2, "Bagnole2")]
        public void TestGetById_Réussit_MultiTest(int idx,string nomx)
        {         
            var resault = _vs.getByID(idx);
            Assert.AreEqual(nomx, resault.Nom);
        }
        [Test]
        public void TestGetById_Réussit_ComparerLesMessages()
        {
            Vehicule searched = new Vehicule() { ID = 2, Nom = "Bagnole2", Immatriculation = "IMM2222" };
            var resault = _vs.getByID(searched.ID);
            string re1 = _vs.CreerMessagePourUnVehicule(searched);
            string re2 = _vs.CreerMessagePourUnVehicule(resault);
            Assert.AreEqual(re1, re2);
        }
        [Test]
        public void TestGetById_Inférieure_A_0_Leve_InvalidIDException()
        {
            try
            { 
            Vehicule searched = new Vehicule() { ID = 0, Nom = "202", Immatriculation = "dqs554" };
            var resault = _vs.getByID(searched.ID);
             Assert.Fail("Impossible d'avoir Id inférieur de 0");
            }
            catch(InvalidIDException e)
            {
              
            }
        }
        [Test]
        public void TestGetById_null_Leve_VehiculeNotFoundException()
        {
            try
            {
                Vehicule searched = new Vehicule() { ID = 5, Nom = "Bagnole", Immatriculation = "JHGBN?5" };
                var resault = _vs.getByID(searched.ID);
                Assert.Fail("Cette véhicule n'éxiste pas");
            }
            catch (VehiculeNotFoundException e)
            {

            }
        }
        [Test]
        public void tGetById_null_Leve_VehiculeNotFoundException()
        {
            var v = new Vehicule();
            v.ID = 66;
            v.Immatriculation = "ffds55";
            v.Nom = "Dacia";
            var ss = _vs.getAll();
            ss.Add(v);
            var x = _vs.getByID(v.ID);
            Assert.AreEqual(x.Immatriculation, v.Immatriculation);
        }
        //SearchById c'est pas une méthode privée pour la tester
    }
}