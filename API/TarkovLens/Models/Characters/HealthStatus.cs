using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TarkovLens.Helpers.ExtensionMethods;

namespace TarkovLens.Models.Characters
{
    public class HealthStatus
    {
        public HealthStatus(
            BodyPart head,
            BodyPart thorax,
            BodyPart stomach,
            BodyPart leftArm,
            BodyPart rightArm,
            BodyPart leftLeg,
            BodyPart rightLeg)
        {
            Head = head;
            Thorax = thorax;
            Stomach = stomach;
            LeftArm = leftArm;
            RightArm = rightArm;
            LeftLeg = leftLeg;
            RightLeg = rightLeg;
        }

        public int MaxHp => this.GetMaxHp();

        [JsonIgnore]
        public int CurrentHp => this.GetCurrentHp();

        public BodyPart Head { get; set; }
        public BodyPart Thorax { get; set; }
        public BodyPart Stomach { get; set; }
        public BodyPart LeftArm { get; set; }
        public BodyPart RightArm { get; set; }
        public BodyPart LeftLeg { get; set; }
        public BodyPart RightLeg { get; set; }

        private int GetMaxHp()
        {
            int maxHp = 0;
            maxHp += this.Head.IsNotNull() ? this.Head.MaxHp : 0;
            maxHp += this.Thorax.IsNotNull() ? this.Thorax.MaxHp : 0;
            maxHp += this.Stomach.IsNotNull() ? this.Stomach.MaxHp : 0;
            maxHp += this.LeftArm.IsNotNull() ? this.LeftArm.MaxHp : 0;
            maxHp += this.RightArm.IsNotNull() ? this.RightArm.MaxHp : 0;
            maxHp += this.LeftLeg.IsNotNull() ? this.LeftLeg.MaxHp : 0;
            maxHp += this.RightLeg.IsNotNull() ? this.RightLeg.MaxHp : 0;
            return maxHp;
        }

        private int GetCurrentHp()
        {
            int currentHp = 0;
            currentHp += this.Head.IsNotNull() ? this.Head.CurrentHp : 0;
            currentHp += this.Thorax.IsNotNull() ? this.Thorax.CurrentHp : 0;
            currentHp += this.Stomach.IsNotNull() ? this.Stomach.CurrentHp : 0;
            currentHp += this.LeftArm.IsNotNull() ? this.LeftArm.CurrentHp : 0;
            currentHp += this.RightArm.IsNotNull() ? this.RightArm.CurrentHp : 0;
            currentHp += this.LeftLeg.IsNotNull() ? this.LeftLeg.CurrentHp : 0;
            currentHp += this.RightLeg.IsNotNull() ? this.RightLeg.CurrentHp : 0;
            return currentHp;
        }
    }
}
