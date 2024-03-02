using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using System.Linq;
public class CardCreator : MonoBehaviour
{
    [SerializeField] CardInfo cardInfo = null;
    [SerializeField] CardResourceIcons_SO allCardResourceIcons = null;
    //Art
    Art art = null;
    CardResourceIcon cardResourceIcon = null;

    CardName titleCardName = null;

    CardSkillDescription cardSkillDescription = null;
    CardTypeName cardTypeName = null;
    ResourceCostText resourceCostText = null;
    CardAttackAmount cardAttackAmount = null;
    CardHealthAmount cardHealthAmount = null;

    ArtBackGround artBackGround = null;
    CardTitleBackGroundColor cardTitleBackGroundColor = null;
    CardTypeBackGroundColor cardTypeBackGroundColor = null;
    CardStatsBackGroundColor cardStatsBackGroundColor = null;

    public event Action onInit = null;
    private void Awake()
    {

    }
    void Start()
    {
        LateInit();
    }
    private void LateInit()
    {
        Invoke(nameof(Init), 0.1f);
        Invoke(nameof(InitCard), 0.11f);

    }

    public void Init()
    {
        artBackGround = GetComponentInChildren<ArtBackGround>();
        art = GetComponentInChildren<Art>();

        titleCardName = GetComponentInChildren<CardName>();
        cardTypeName = GetComponentInChildren<CardTypeName>();

        cardSkillDescription = GetComponentInChildren<CardSkillDescription>();
        resourceCostText = GetComponentInChildren<ResourceCostText>();
        cardAttackAmount = GetComponentInChildren<CardAttackAmount>();
        cardHealthAmount = GetComponentInChildren<CardHealthAmount>();


        cardResourceIcon = GetComponentInChildren<CardResourceIcon>();
        cardTitleBackGroundColor = GetComponentInChildren<CardTitleBackGroundColor>();
        cardTypeBackGroundColor = GetComponentInChildren<CardTypeBackGroundColor>();
        cardStatsBackGroundColor = GetComponentInChildren<CardStatsBackGroundColor>();
        cardSkillDescription.Tmp.text = string.Empty;

    }

    public void InitCard()
    {
        if (cardInfo == null)
        {
            Debug.Log("failed to find cardinfo");
            return;
        }

        SetCardResourceCost(cardInfo.CardResourceCostRef);  // int
        SetCardAttackAmount(cardInfo.CardAttackRef);
        SetCardHealthAmount(cardInfo.CardHealthRef);

        SetSprite(art, cardInfo.CardArtSpriteRef);
        SwitchCardColor();

        //titleCardName.SetTitleName(cardInfo.CardTitle);
        SetCardTitleName(titleCardName, cardInfo.CardTitle);
        SwitchCardSkills();
        SwitchCardTypeName();


    }
    private void SwitchCardSkills()
    {
        int _size = cardInfo.AllCardSkills.Count;
        for (int i = 0; i < _size; i++)
        {
            switch (cardInfo.AllCardSkills[i])
            {
                case API_CardSkills.CardSkill.Burn:
                    SetCardSkillText(API_CardSkills.burnString);
                    break;
                case API_CardSkills.CardSkill.Fly:
                    SetCardSkillText(API_CardSkills.flyString);
                    break;
                case API_CardSkills.CardSkill.Poison:
                    SetCardSkillText(API_CardSkills.poisonString);
                    break;
                case API_CardSkills.CardSkill.DeathTouch:
                    SetCardSkillText(API_CardSkills.deathTouchString);
                    break;
                case API_CardSkills.CardSkill.LifeTouch:
                    SetCardSkillText(API_CardSkills.lifeTouchString);
                    break;
                case API_CardSkills.CardSkill.FirstAttack:
                    SetCardSkillText(API_CardSkills.firstAttackString);
                    break;
                case API_CardSkills.CardSkill.NONE:
                    SetCardSkillText(API_CardSkills.none);
                    break;

            }

        }
    }
    private void SwitchCardTypeName()
    {

        switch (cardInfo.CardTypeRef)
        {
            case CardType.Instant:
                SetCardTitleName(cardTypeName, CardType.Instant.ToString());
                break;
            case CardType.Sorcery:
                SetCardTitleName(cardTypeName, CardType.Sorcery.ToString());
                break;
            case CardType.Creature:
                SetCardTitleName(cardTypeName, CardType.Creature.ToString());
                break;
                case CardType.Resource:
                SetCardTitleName(cardTypeName, CardType.Resource.ToString());
                break;
             

            
        }
    }
            

        
    
    private void SwitchCardColor()
    {
        switch (cardInfo.ResourceTypeRef)
        {
            case ResourceType.None:
                break;
            case ResourceType.Fire:
                SetAllCardBackGroundColors(API_CardColors.fireColor);
                SetSprite(cardResourceIcon, allCardResourceIcons.fireIconSprite);
                break;
            case ResourceType.Air:
                SetAllCardBackGroundColors(API_CardColors.airColor);
                SetSprite(cardResourceIcon, allCardResourceIcons.airIconSprite);

                break;
            case ResourceType.Earth:
                SetAllCardBackGroundColors(API_CardColors.earthColor);
                SetSprite(cardResourceIcon, allCardResourceIcons.earthIconSprite);
                break;
            case ResourceType.Water:
                SetAllCardBackGroundColors(API_CardColors.waterColor);
                SetSprite(cardResourceIcon, allCardResourceIcons.waterIconSprite);
                break;
            case ResourceType.Darkness:
                SetAllCardBackGroundColors(API_CardColors.darknessColor);
                SetSprite(cardResourceIcon, allCardResourceIcons.darknessIconSprite);

                break;
            case ResourceType.Light:
                SetAllCardBackGroundColors(API_CardColors.lightColor);
                SetSprite(cardResourceIcon, allCardResourceIcons.lightIconSprite);

                break;


        }


    }

    private void SetAllCardBackGroundColors(Color _color)
    {
        SetBackGroundColor(artBackGround, _color);
        SetBackGroundColor(cardTitleBackGroundColor, _color);
        SetBackGroundColor(cardStatsBackGroundColor, _color);
        SetBackGroundColor(cardTypeBackGroundColor, _color);
    }

    private void SetBackGroundColor(ArtBackGround _artBackGround, Color _color)
    {
        _artBackGround.ImageRef.color = _color;

    }

    private void SetCardTitleName(Title _title,string _titleString)
    {

        _title.SetTitleName(_titleString);
    }

    private void SetCardResourceCost(int _cost)
    {
        resourceCostText.Tmp.text = _cost.ToString();
    }

    private void SetCardAttackAmount(int _attack)
    {
        cardAttackAmount.Tmp.SetText(_attack.ToString());
    }
    private void SetCardHealthAmount(int _health)
    {
        cardHealthAmount.Tmp.SetText(_health.ToString());
    }
    private void SetSprite(Art _art, Sprite _sprite)
    {
        _art.SetSprite(_sprite);

    }

    private void SetCardSkillText(string _cardSkill)
    {
        
        bool _isEmpty = false;
        if (cardSkillDescription.Tmp.text == cardSkillDescription.PlaceHolderText)
        {
            cardSkillDescription.Tmp.text = "";
            _isEmpty = true;
        }
        cardSkillDescription.Tmp.text += _isEmpty ? "" + _cardSkill : "<br>";  // + _cardSkill
        if (cardSkillDescription.Tmp.text.Contains($"{_cardSkill}")) return;
        cardSkillDescription.Tmp.text += _cardSkill;



    }
}
