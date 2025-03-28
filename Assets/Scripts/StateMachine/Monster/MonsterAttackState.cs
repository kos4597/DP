using Cysharp.Threading.Tasks;
using UnityEngine;
using System.Threading;

public class MonsterAttackState : BaseState
{
    public MonsterAttackState(Monster monster, MonsterStateMachine stateMachine) : base(monster, stateMachine) { }

    private CancellationTokenSource cancellationToken;
    public override void OnStateEnter()
    {
        cancellationToken = new CancellationTokenSource();
        Attack(cancellationToken).Forget();
    }

    public override void OnStateUpdate()
    {
        if (monster.CheckPlayerAttackRange() == false)
        {
            monsterStateMachine?.ChangeState(MonsterStateType.Tracking);
        }
    }

    public override void OnStateExit()
    {
        cancellationToken.Cancel();
        cancellationToken.Dispose();
    }

    private async UniTask Attack(CancellationTokenSource token)
    {
        while(monster.CheckPlayerAttackRange())
        {
            monster.GetComponent<Animator>().SafeSetAnimaion(StringDefine.MONSTER_ATTACK_ANI_HASH);

            if(monster.TrackingTargetTr.tag.Equals("Player"))
            {
                monster.TrackingTargetTr.GetComponent<Player>().Hit(10);
            }

            Debug.LogWarning("Attack");
            await UniTask.Delay(monster.MonsterSO.MonsterData.AttackSpeed, cancellationToken: token.Token);
        }
    }
}
