using UnityEngine;
using UnityEngine.UI;
using MissionAcomplished.Missions.Validators;
using MissionAcomplished.Cards;
using MissionAcomplished.Piles;

namespace MissionAcomplished.Missions
{
    public class MissionCard : MonoBehaviour
    {
        public Sprite missionTextSprite;
        protected Text textComponentCache;
        protected Image imageCache;
        protected MissionDefinition _value;
        protected BaseValidator _validator;

        public MissionDefinition Value
        {
            get => this._value;
            set
            {
                this._value = value;
                this._validator = ValidatorFactory.make(value);
                UpdatePrefab();
            }
        }
        private Text TextComp
        {
            get
            {
                if (this.textComponentCache == null) this.textComponentCache = this.transform.GetChild(0).GetComponent<Text>();
                return this.textComponentCache;
            }
        }
        protected virtual Image ImageComp
        {
            get
            {
                if (this.imageCache == null) this.imageCache = this.GetComponent<Image>();
                return this.imageCache;
            }
        }

        private void UpdatePrefab()
        {
            this.ImageComp.sprite = this.missionTextSprite;
            this.TextComp.text = this._validator.GetCardText();
            this.TextComp.enabled = true;
        }

        public bool IsAcomplished(CardBase[] piles)
        {
            return this._validator.IsAcomplished(piles);
        }
    }
}
