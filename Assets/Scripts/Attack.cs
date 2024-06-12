using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Alteruna;

public class Attack : CommunicationBridge
{
  private Rigidbody2DSynchronizable rb;
  private Rigidbody2DSynchronizable mrb;
  private Alteruna.Avatar _avatar;
  private Vector2 dir;
  private bool golpear;
  private GameObject player;
  public SpriteRenderer sr;
  private RigidbodyConstraints2D rc;

  [SerializeField]
  private Slider energia;
  private int totalEnergia = 100;
  private int energiaActual;
  private bool tieneEnergia = true;

  private WaitForSeconds wfs = new WaitForSeconds(0.1f);
  private Coroutine descansar;
  void Awake()
  {
    _avatar = GetComponent<Alteruna.Avatar>();
    mrb = GetComponent<Rigidbody2DSynchronizable>();
    rc = mrb.Rigidbody.constraints;
    golpear = false;

    energiaActual = totalEnergia;
    energia.value = totalEnergia;
  }



  void Update()
  {
    if (!_avatar.IsMe)
    {
      return;
    }

    if (energiaActual <= 0)
    {
      tieneEnergia = false;
    }

    if ((SimpleInput.GetKey(KeyCode.H) || SimpleInput.GetButton("H")) && tieneEnergia)
    {
      usandoEnergia(1);
      sr.material.color = Color.green;
      mrb.Rigidbody.constraints = RigidbodyConstraints2D.FreezePosition;

      if (golpear && Input.GetKeyUp(KeyCode.D))
      {
        rb.AddForce(Vector2.right * 10, ForceMode.Impulse);
      }
      else if (golpear && Input.GetKeyUp(KeyCode.A))
      {
        rb.AddForce(Vector2.left * 10, ForceMode.Impulse);
      }
      else if (golpear && Input.GetKeyUp(KeyCode.S))
      {
        rb.AddForce(Vector2.down * 10, ForceMode.Impulse);
      }
      else if (golpear && Input.GetKeyUp(KeyCode.W))
      {

        rb.AddForce(Vector2.up * 10, ForceMode.Impulse);
      }

    }
    if ((Input.GetKeyUp(KeyCode.H) || SimpleInput.GetButtonUp("H")) || !tieneEnergia)
    {
      mrb.Rigidbody.constraints = rc;
      sr.material.color = Color.white;
    }
  }


  void usandoEnergia(int a)
  {
    if (energiaActual - a >= 0)
    {
      energiaActual -= a;
      energia.value = energiaActual;

      if (descansar != null)
      {
        StopCoroutine(descansar);
      }
      descansar = StartCoroutine(regenerarEnergia());
    }
  }

  private IEnumerator regenerarEnergia()
  {
    yield return new WaitForSeconds(2);

    while (energiaActual < totalEnergia)
    {
      energiaActual += totalEnergia / 10;
      energia.value = energiaActual;
      yield return wfs;
    }
    descansar = null;
    tieneEnergia = true;
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Enemigo") && !golpear)
    {
      rb = other.gameObject.GetComponent<Rigidbody2DSynchronizable>();
      golpear = true;
    }
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.CompareTag("Enemigo")) golpear = false;
  }

}
