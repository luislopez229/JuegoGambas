using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;
using UnityEngine.UI;

public class PlayerMovement : CommunicationBridge
{
  // Start is called before the first frame update
  private Rigidbody2DSynchronizable rb;
  public float salto;
  private int cantidadSaltos;
  private Alteruna.Avatar _avatar;
  public SpriteRenderer sr;
  private InputField go;

  void Start()
  {

    rb = GetComponent<Rigidbody2DSynchronizable>();
    _avatar = GetComponent<Alteruna.Avatar>();
    cantidadSaltos = 2;

    this.GetComponentInChildren<TextMesh>().text = _avatar.Possessor.Name;
    if (!_avatar.IsMe)
    {
      _avatar.tag = "Enemigo";
      sr.material.color = Color.red;
      GameObject.FindWithTag("Enemigo").GetComponentInChildren<TextMesh>().text = _avatar.Possessor.Name + ", "+_avatar.tag;
      return;
    }
  }


  // Update is called once per frame
  void FixedUpdate()
  {
    if (!_avatar.IsMe)
    {
      return;
    }
    else
    {

      Vector2 mover = new Vector2(SimpleInput.GetAxis("Horizontal"), 0).normalized;
      if (mover.x < 0)
      {
        sr.flipX = true;
      }
      else
      {
        sr.flipX = false;
      }
      rb.AddForce(mover * 20);
    }
  }

  void Update()
  {
    if (!_avatar.IsMe)
    {
      return;
    }
    else
    {

      if (SimpleInput.GetButtonDown("Jump"))
      {
        if (cantidadSaltos > 0)
        {
          rb.AddForce(Vector2.up * 10, ForceMode.Impulse);
          cantidadSaltos--;
        }
      }


    }
  }

  private void OnCollisionEnter2D(Collision2D col)
  {
    if (col.gameObject.CompareTag("Suelo"))
    {
      cantidadSaltos = 2;
    }
  }
}