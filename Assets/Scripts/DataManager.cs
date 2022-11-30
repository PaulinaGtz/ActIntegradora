using System.Collections;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField]
    float delayTime = 1.0f;
    [SerializeField]
    private Carro[] _carros;
    WaitForSeconds delay = new WaitForSeconds(0.5f);
    [SerializeField]
    private Semaforo[] _semaforos;

    public GameObject[] _carrosGO;
    private GameObject[] _semaforosGO;
    private Step[] _frames;
    private int carCounter;
    private int semaforoCounter;

    // Start is called before the first frame update
    private void Inicio()
    {
        _carrosGO = new GameObject[ _carros.Length];
        // _semaforosGO = new GameObject[_semaforos.Length];
        carCounter = 0;
        
        for (int i = 0; i < _carros.Length; i++)
        {
            _carrosGO[i] = CarPoolManager.Instance.ActivarObjeto(Vector3.zero);

        }

        
    }

    private void PosicionarCarros()
    {
      
        
        for (int i = 0; i < _carros.Length; i++)
        {
           
            _carrosGO[i].transform.position = new Vector3(
                _carros[i].x, 1, _carros[i].z);
            
          
        }
    }

  

    IEnumerator CambiarPosicion(GeneralInfo datos)
    {
        for (int i = 0; i < _carros.Length; i++) {
            //_carrosGO[i].transform.position.y = 0.6;
            if (_carros[i].dir == 180) {
                _carrosGO[i].transform.Rotate(0,180f,0, Space.Self);
            }else if (_carros[i].dir == 90) {
                _carrosGO[i].transform.Rotate(0,90f,0, Space.Self);
            }else if (_carros[i].dir == -90) {
                _carrosGO[i].transform.Rotate(0,-90f,0, Space.Self);
            }
        }
        
        for (int i = 0; i < datos.frames.Length; i++)
        {
            _carros = datos.frames[i].cars;
            PosicionarCarros();
            yield return new WaitForSeconds(0.01f);
        }
    }


    public void EscucharRequestSinArgumentos()
    {
        print("HUBO UN REQUEST MUY INTERESANTE!");
    }

    public void EscucharRequestConArgumentos(GeneralInfo datos)
    {
        print("DATOS: " + datos);


        _carros = datos.cars;
        _semaforos = datos.semaphores;
        _frames = datos.frames;
       
        Inicio();
        StartCoroutine(CambiarPosicion(datos));
     
    }
}