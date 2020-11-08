using System;
using System.Collections.Generic;
using ProjetPourTU.Model;
using ProjetPourTU.Services;

using System.Text;
using ProjetPourTU.Services.CustomExceptions;

namespace ProjetTU_Test.Mock
{
    class VehiculeServiceMock : VehiculeService
    {
        private List<Vehicule> _mesVehiculemock;
        public VehiculeServiceMock(){}
     
        public override List<Vehicule> getAll()
        {
            if (_mesVehiculemock == null)
            {
                _mesVehiculemock = new List<Vehicule>();
                _mesVehiculemock.Add(new Vehicule() { ID = 1, Nom = "Bagnole1", Immatriculation = "IMM1111" });
                _mesVehiculemock.Add(new Vehicule() { ID = 2, Nom = "Bagnole2", Immatriculation = "IMM2222" });
                _mesVehiculemock.Add(new Vehicule() { ID = 3, Nom = "Bagnole3", Immatriculation = "IMM3333" });
            } 
                return _mesVehiculemock;
        }
        public override string CreerMessagePourUnVehicule(Vehicule v)
        {
            return base.CreerMessagePourUnVehicule(v);
        }
       
        public override string CreerMessage()
        {
            return base.CreerMessage();
        }
        public override Vehicule getByID(int VehiculeID)
        {
            return base.getByID(VehiculeID);
        }
        public override void AddVehicule(Vehicule nouveauVehicule)
        {    
                base.AddVehicule(nouveauVehicule);
        }

    }
}
