using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] MyBoxCollider2D enemyCollider;
    [SerializeField] MyRigidBody2D enemyRigidBody;
    
    [SerializeField] float _enemySpeed;
    public float EnemySpeed { get { return _enemySpeed; } private set { _enemySpeed = value; } }
    public Health EnemyHealth { get; set; }
    public HealthBarFollow HealthBarFollow;

    [SerializeField] float _timer;
    [SerializeField] float _cooldownToGoBackToCastle;
    bool _isTargetingCastle = true;

    private void Start()
    {
        enemyCollider.OnCollisionEnter += OnMyCollisionEnter2D;
        enemyCollider.OnCollisionExit += OnMyCollisionExit2D;
        EnemyHealth.OnDeath += OnEnemiesDeath;
    }
    private void FixedUpdate()
    {
            MoveTowardsCastle();
        if (!_isTargetingCastle)
            TimerToChargeCastle();

    }

    void MoveTowardsCastle() 
    {
        enemyRigidBody.velocity = new Vector2(_enemySpeed, enemyRigidBody.velocity.y); //speed directed to castle
    }

    void OnMyCollisionExit2D(MyBoxCollider2D collider) 
    {
       
        if (collider.CompareTag("Castle"))
        {
             GameManager.Instance.PlayerCastleHealth.GotHurt(EnemyHealth.Damage);//do it so it will get health and do the logic of values inside instead of getting damage
        }
        if (collider.CompareTag("PlayersWeapon"))
        {
            Physics2DManager.Instance._myBoxColliders2D.Remove(collider);
            Destroy(collider.gameObject);
            EnemyHealth.GotHurt(GameManager.Instance.PlayerCastleHealth.Damage);
        }
        //enemyCollider.OnCollisionEnter
    }
    void OnMyCollisionEnter2D(MyBoxCollider2D collider)
    {
        if (collider.CompareTag("Castle"))
        {
            transform.position += Vector3.right*0.1f;
            _enemySpeed *= -1;
            _isTargetingCastle =false;
        }
        enemyCollider.SetOnCollisionWith(CanCollideWithCollider);
    }

    bool CanCollideWithCollider(MyBoxCollider2D collider)//true is cant, false is not
    {
        if (collider.CompareTag("Enemy") && enemyCollider.CompareTag("Enemy"))
            return false;
        return true;
    }
    void TimerToChargeCastle() 
    {
        _timer += (1/50f);
        if(_timer >= _cooldownToGoBackToCastle)
        {
            _enemySpeed *= -1;
            _timer = 0;
            _isTargetingCastle = true;
        }
    }

    private void OnEnemiesDeath() 
    {
        Destroy(this.HealthBarFollow.gameObject);
        Physics2DManager.Instance._myBoxColliders2D.Remove(enemyCollider);
        Destroy(gameObject);
    }

}
