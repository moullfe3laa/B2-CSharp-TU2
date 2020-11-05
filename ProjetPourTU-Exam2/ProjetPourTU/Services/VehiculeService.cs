using ProjetPourTU.Model;
using ProjetPourTU.Services.CustomExceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace ProjetPourTU.Services {
    public class VehiculeService {

        private List<Vehicule> _mesVehicules;

        private const string jsonFile = "//cheminreseauinconnu/myFile.json";
        /// <summary>
        /// Constructeur => pour plus de simplicité, j'initialise 3 véhicules dans le service
        /// </summary>
        public VehiculeService() {
            
        }



        public List<Vehicule> getAll() {
            if (_mesVehicules == null) {
                if (!File.Exists(jsonFile)){
                    _mesVehicules = new List<Vehicule>();
                } else {
                    using (var sr = new StreamReader(jsonFile)) {
                        _mesVehicules = JsonSerializer.Deserialize<List<Vehicule>>(jsonFile);
                    }
                }
                
            }
            return _mesVehicules;
            
        }

        public void Save() {
            using (var sr = new StreamWriter(jsonFile)) {
                sr.Write(JsonSerializer.Serialize<List<Vehicule>>(_mesVehicules));
            }
        }
        /// <summary>
        /// Retourne un véhicule par son identifiant
        /// Lève une exception si l'ID est inférieur ou égal à 0
        /// Lève une exception si le véhicule n'existe pas
        /// </summary>
        /// <param name="VehiculeID"></param>
        /// <returns></returns>
        public Vehicule getByID(int VehiculeID) {
            if (VehiculeID <= 0)
                throw new InvalidIDException();
            Vehicule result = searchByID(VehiculeID);
            if (result == null)
                throw new VehiculeNotFoundException();
            return result;
        }


        /// <summary>
        /// Ajoute un véhicule
        /// Vérifie si un même véhicule existe avec l'ID, dans ce cas, on lève une exception
        /// </summary>
        /// <param name="nouveauVehicule"></param>
        public void AddVehicule(Vehicule nouveauVehicule) {
            if (nouveauVehicule == null)
                throw new NullNotAllowedException();
            Vehicule memeVehiculeParID = searchByID(nouveauVehicule.ID);
            if (memeVehiculeParID != null)
                throw new SameIDExistsException();
            // création d'un nouvel ID
            int maxID = 1;
            List<Vehicule> vehicules = getAll();
            foreach(Vehicule v in vehicules) {
                if (v.ID >= maxID) {
                    maxID = v.ID + 1;
                }
            }
            nouveauVehicule.ID = maxID;
            vehicules.Add(nouveauVehicule);
            Save();
        }



        /// <summary>
        /// Crée un message à partir d'un véhicule
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public string CreerMessagePourUnVehicule(Vehicule v) {
            return "Véhicule : " + v.Nom + ", immatriculation : " + v.Immatriculation;
        }


        /// <summary>
        /// Création d'un message pour l'ensemble des véhicules de la bd
        /// </summary>
        public string CreerMessage() {
            // ne cherchez pas à comprendre cette ligne, je vous l'expliquerai ultérieurement
            // vous devez simplement produire le test correspondant à cette méthode (qui fonctionne correctement)
            return string.Join("\n",getAll().Select(v => CreerMessagePourUnVehicule(v)));
        }
        /// <summary>
        /// Cherche un vehicule par son id, retourne null si le véhicule n'a pas été trouvé
        /// </summary>
        /// <param name="VehiculeID"></param>
        /// <returns></returns>
        private Vehicule searchByID(int VehiculeID) {
            Vehicule result = null;
            // parcours des véhicules pour en trouver un avec le même ID
            foreach (Vehicule v in getAll()) {
                if (v.ID == VehiculeID) {
                    result = v;
                    break;
                }
            }
            return result;
        }
    }
}
