using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Wavespawner : MonoBehaviour {

    //public List<GameObject> Spawnpositionen_List = new List<GameObject>();
    //public GameObject Wegpunkt_ordner_Gamobjekt;
    //public GameObject Endpunkt;
    //public List<GameObject> Gegner_Liste = new List<GameObject>();
    //[Space(15)]
    //public Text WaveCountdownText_Text;
    //public float ZeitZwischenWaves = 5f;
    //public float countdown = 2f;
    //public float Wartezeitzwischengegnerspawn = 0.5f;
    //[Space(15)]
    //public Text WaveCounter_Text;
    //public int Wave_index_max = 50;
    //public int wave_Index = 0;
    //[Space(15)]
    //public Text Leben_endpunkt_Text;
    //public float Leben_endpunkt = 25;
    //public bool Spawnstop_Leben_endpunkt_null = false;
    //[Space(15)]
    //public Text Status_Text;
    //public string Status_Gewonnen_string = "Du hast es geschaft";
    //public string Status_Verloren_string = "Versager";
    //[Space(15)]
    //[Header("Werteüberwachung (Script interne Werte)")]
    //[Tooltip("Darf_Ich_überhaupt_schon_arbeiten")]
    //public bool Darf_Ich_überhaupt_schon_arbeiten = false;
    //[Tooltip("Darf_Ich_überhaupt_schon_arbeiten_static")]
    //public bool Darf_Ich_überhaupt_schon_arbeiten_static;
    //public bool Wurde_schon_aktiviert = false;
    //[Space(15f)]
    //[Header("Script internes Setup")]
    //private GameObject Gegnerobjekt;
    //public static GameObject Gegnerobjekt_static;

    //private void Start()
    //{
    //    Leben_endpunkt_Text.text = Leben_endpunkt.ToString();
    //}

    //private void Update()
    //{
    //    Gegnerobjekt_static = Gegnerobjekt;
    //    Darf_Ich_überhaupt_schon_arbeiten_static = Darf_Ich_überhaupt_schon_arbeiten;

    //    if(Gegnerobjekt == null && Darf_Ich_überhaupt_schon_arbeiten == true )
    //    {
    //        if (Leben_endpunkt <= 0)
    //        {

    //            Darf_Ich_überhaupt_schon_arbeiten = false;
    //            Status_Text.text = Status_Verloren_string;
    //            if(Spawnstop_Leben_endpunkt_null)
    //            {
    //                StopCoroutine(SpawnWave());
    //            }
    //            FindObjectOfType<Charakter_Bewegung_third>().Levelneustarten(5);
    //            return;
    //        }

    //        WaveCountdownText_Text.text = Mathf.Ceil(countdown).ToString(); //-> Rundet auf den nächst größeren Int-Wert
    //        WaveCounter_Text.text = wave_Index.ToString();

    //        if (countdown <= 0f)
    //        {
    //            StartCoroutine(SpawnWave());
    //            countdown = ZeitZwischenWaves;
    //            return;
    //        }
    //        countdown -= Time.deltaTime;
    //    }

    //    if (Gegnerobjekt == null &&  wave_Index >= Wave_index_max)
    //    {
    //        Status_Text.text = Status_Gewonnen_string;
    //        FindObjectOfType<Charakter_Bewegung_third>().Levelneustarten(5);
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (Wurde_schon_aktiviert == false)
    //    {
    //        Darf_Ich_überhaupt_schon_arbeiten = true;
    //        this.gameObject.GetComponent<Collider>().enabled = false;
    //    }
    //}

    //IEnumerator SpawnWave()
    //{
    //    wave_Index++;

    //    for (int i = 0; i < wave_Index; i++)
    //    {
    //        SpawnGEgner();
    //        yield return new WaitForSeconds(Wartezeitzwischengegnerspawn);
    //    }

    //    if(wave_Index >= Wave_index_max)
    //    {
    //        Darf_Ich_überhaupt_schon_arbeiten = false;
    //    }
    //}

    //void SpawnGEgner()
    //{
    //    int temp_pos = Random.Range(0, Spawnpositionen_List.Count);
    //    int Random_Gegner = Mathf.RoundToInt(Random.Range(0, Gegner_Liste.Count));
    //    Gegnerobjekt = Instantiate(Gegner_Liste[Random_Gegner], Spawnpositionen_List[temp_pos].transform.position, Spawnpositionen_List[temp_pos].transform.rotation);
    //    Gegnerobjekt.GetComponent<Wave_Enemy>().Wegpunkte_Liste = Spawnpositionen_List[temp_pos].GetComponent<Waypoint>().Wegpunkte_Liste;
    //    Gegnerobjekt.GetComponent<Wave_Enemy>().WaveGegner_Inizialisirung();
    //}

    //public void Endpunkt_Lebensabzug(float Schaden)
    //{
    //    Leben_endpunkt -= Schaden;
    //    Leben_endpunkt_Text.text = Leben_endpunkt.ToString();
    //}

    //public void Setzen_auf_true___Darf_Ich_überhaupt_schon_arbeiten()
    //{
    //    Darf_Ich_überhaupt_schon_arbeiten = true;
    //}

    //public void Setzen_auf_False___Darf_Ich_überhaupt_schon_arbeiten()
    //{
    //    Darf_Ich_überhaupt_schon_arbeiten = false;
    //}
}
