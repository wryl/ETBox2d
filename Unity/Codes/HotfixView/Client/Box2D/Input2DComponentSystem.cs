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

        public static bool CmdContain(CmdType curr, CmdType target)
        {
             return (curr & target) == target;
        }
        public static void Update(this Input2DComponent self)
        {
            var currcmdtype = self.MyUnit2D.GetComponent<Controller2DComponent>().CurrCmdType;
            if (Input.GetKeyDown(KeyCode.A)&&!CmdContain(currcmdtype,CmdType.A))
            {
                currcmdtype |= CmdType.A;
            }

            if (Input.GetKeyDown(KeyCode.D)&&!CmdContain(currcmdtype,CmdType.D))
            {
                currcmdtype |= CmdType.D;
            }
            if (Input.GetKeyUp(KeyCode.A)&&!CmdContain(currcmdtype,CmdType.AUp))
            {
                currcmdtype |= CmdType.AUp;
            }

            if (Input.GetKeyUp(KeyCode.D)&&!CmdContain(currcmdtype,CmdType.DUp))
            {
                currcmdtype |= CmdType.DUp;
            }
            if (Input.GetKeyDown(KeyCode.LeftShift)&&!CmdContain(currcmdtype,CmdType.LeftShift))
            {
                currcmdtype |= CmdType.LeftShift;
            }
            if (Input.GetKeyDown(KeyCode.Space)&&!CmdContain(currcmdtype,CmdType.Space))
            {
                currcmdtype |= CmdType.Space;
            }
            if (Input.GetKeyUp(KeyCode.Space)&&!CmdContain(currcmdtype,CmdType.SpaceUp))
            {
                currcmdtype |= CmdType.SpaceUp;
            }
            if (Input.GetKeyDown(KeyCode.J)&&!CmdContain(currcmdtype,CmdType.J))
            {
                currcmdtype |= CmdType.J;
            }
            self.MyUnit2D.GetComponent<Controller2DComponent>().CurrCmdType = currcmdtype;
        }
    }
}