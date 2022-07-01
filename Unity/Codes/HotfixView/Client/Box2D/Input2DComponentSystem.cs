using System;
using System.Numerics;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace ET.Client
{
    [FriendOf(typeof(Input2DComponent))]
    [FriendOfAttribute(typeof(CharacterhorizontalMoveComponent))]
    public static class Input2DComponentSystem
    {
        [ObjectSystem]
        public class Input2DComponentAwakeSystem : AwakeSystem<Input2DComponent>
        {
            public override void Awake(Input2DComponent self)
            {
                self.MyUnit2D = self.GetParent<Unit2D>();
            }
        }

        [ObjectSystem]
        public class Input2DComponentUpdateSystem : UpdateSystem<Input2DComponent>
        {
            public override void Update(Input2DComponent self)
            {
                self.Update();
            }
        }


        public static void Update(this Input2DComponent self)
        {
            CmdType cmdType = CmdType.Idle;
            if (Input.GetKey(KeyCode.A))
            {
                cmdType |= CmdType.A;
            }

            if (Input.GetKey(KeyCode.D))
            {
                cmdType |= CmdType.D;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                cmdType |= CmdType.LeftShift;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                cmdType |= CmdType.Space;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                cmdType |= CmdType.SpaceUp;
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                cmdType |= CmdType.J;
            }
            self.MyUnit2D.GetComponent<Controller2DComponent>().CurrCmdType = cmdType;
        }
    }
}