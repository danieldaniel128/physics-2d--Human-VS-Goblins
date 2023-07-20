using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightConroller : MonoBehaviour//make abstract class later
{
    [SerializeField] MyBoxCollider2D _knightCollider;
    [SerializeField] MyRigidBody2D _knightRigidBody;

    public float _knightSpeed;
    public float KnightSpeed { get { return _knightSpeed; } set { _knightSpeed = value; } }
    public Health KnightHealth { get; set; }
    public HealthBarFollow KnightHealthBarFollow;

    [SerializeField] float _timer;
    [SerializeField] float _cooldownToGoBackToCastle;
    bool _isTargetingCastle = true;

    private void Start()
    {
        _knightCollider.OnCollisionEnter += OnMyCollisionEnter2D;
        _knightCollider.OnCollisionExit += OnMyCollisionExit2D;
        KnightHealth.OnDeath += OnEnemiesDeath;
    }
    private void FixedUpdate()
    {
        MoveTowardsCastle();
        if (!_isTargetingCastle)
            TimerToChargeCastle();
    }

    void MoveTowardsCastle()
    {
        _knightRigidBody.velocity = new Vector2(_knightSpeed, _knightRigidBody.velocity.y); //speed directed to castle
    }

    void OnMyCollisionExit2D(MyBoxCollider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            collider.GetComponent<EnemyController>().EnemyHealth.GotHurt(KnightHealth.Damage);
            Debug.Log("Before Hurt hp:" + KnightHealth.CurrnetHP);
            KnightHealth.GotHurt(collider.GetComponent<EnemyController>().EnemyHealth.Damage);
            Debug.Log("Hurt");
            Debug.Log("Health:" + KnightHealth.CurrnetHP);
            Debug.Log("TookDamage:" + collider.GetComponent<EnemyController>().EnemyHealth.Damage);
        }

        //enemyCollider.OnCollisionEnter
    }
    void OnMyCollisionEnter2D(MyBoxCollider2D collider)
    {
        if (collider.tag.Equals("EnemyCastle"))
        {
            transform.position += Vector3.left * 0.1f;
            _knightSpeed *= -1;
            _isTargetingCastle = false;
        }
        _knightCollider.SetOnCollisionWith(CanCollideWithCollider);
    }

    bool CanCollideWithCollider(MyBoxCollider2D collider)//true is cant, false is not
    {
        if (collider.CompareTag("Knight") && _knightCollider.CompareTag("Knight"))
            return false;
        if (collider.CompareTag("PlayersWeapon")/* && _knightCollider.CompareTag("PlayersWeapon")*/)
            return false;
        return true;
    }
    void TimerToChargeCastle()
    {
        _timer += (1 / 50f);
        if (_timer >= _cooldownToGoBackToCastle)
        {
            _knightSpeed *= -1;
            _timer = 0;
            _isTargetingCastle = true;
        }
    }

    private void OnEnemiesDeath()
    {
        Destroy(this.KnightHealthBarFollow.gameObject);
        Physics2DManager.Instance._myBoxColliders2D.Remove(_knightCollider);
        Destroy(gameObject);
    }
}
