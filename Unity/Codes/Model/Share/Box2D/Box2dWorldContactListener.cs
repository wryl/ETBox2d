using System;
using Box2DSharp.Collision.Collider;
using Box2DSharp.Dynamics;
using Box2DSharp.Dynamics.Contacts;
namespace ET
{
    public class Box2dWorldContactListener:IContactListener
    {

        public Box2dWorldContactListener(Box2dWorldComponent world)
        {
            this.worldComponent = world;
        }
        private Box2dWorldComponent worldComponent;
        public void BeginContact(Contact contact)
        {
            if (this.worldComponent.bodyComponents.ContainsKey(contact.FixtureA.Body) && this.worldComponent.bodyComponents.ContainsKey(contact.FixtureB.Body))
            {
                var bodyA = this.worldComponent.bodyComponents[contact.FixtureA.Body];
                var bodyB = this.worldComponent.bodyComponents[contact.FixtureB.Body];
                Game.EventSystem.Callback(CallbackType.Box2DBeginContact,bodyA,bodyB);
                Game.EventSystem.Callback(CallbackType.Box2DBeginContact,bodyB,bodyA);
            }
        }

        public void EndContact(Contact contact)
        {
            if (this.worldComponent.bodyComponents.ContainsKey(contact.FixtureA.Body) && this.worldComponent.bodyComponents.ContainsKey(contact.FixtureB.Body))
            {
                var bodyA = this.worldComponent.bodyComponents[contact.FixtureA.Body];
                var bodyB = this.worldComponent.bodyComponents[contact.FixtureB.Body];
                Game.EventSystem.Callback(CallbackType.Box2DEndContact,bodyA,bodyB);
                Game.EventSystem.Callback(CallbackType.Box2DEndContact,bodyB,bodyA);
            }
        }

        public void PreSolve(Contact contact, in Manifold oldManifold)
        {
        }

        public void PostSolve(Contact contact, in ContactImpulse impulse)
        {
        }
    }
}