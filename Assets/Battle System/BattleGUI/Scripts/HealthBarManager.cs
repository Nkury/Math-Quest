using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBarManager : MonoBehaviour {

    internal readonly float HP_ICON_OFFSET = 2.5f;

    public int IconHeight;
    public int IconWidth;
    public int NumberOfHeartsVisible;
    public Sprite DefaultSprite;
    public Sprite OverrideSprite;
    public bool IsLeftToRight;

	// Use this for initialization
	void Start ()
    {
        Hearts = gameObject.GetComponentsInChildren<Image>();
        //InitHealthBar(NumberOfHeartsVisible);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// Initializes the health bar with a set number of sprites.
    /// <remarks>
    /// This should be called by the Script that manages the battle scene.
    /// </remarks>
    /// </summary>
    /// <param name="withNumberOfHearts">The number of icons (hearts) to display on the health bar.</param>
    internal void InitHealthBar(int withNumberOfHearts)
    {
        NumberOfHeartsVisible = withNumberOfHearts;
        for(int i = 0; i < Capacity; i++)
        {
            if (i < NumberOfHeartsVisible)
            {
                Image icon = Hearts[i];
                icon.rectTransform.sizeDelta = new Vector2(IconWidth, IconHeight);
                icon.sprite = DefaultSprite;
                float iconOffset = IconWidth * i;
                float icon_X = gameObject.transform.position.x + iconOffset + (i > 0 ? HP_ICON_OFFSET * i : 0);
                icon.transform.position = new Vector3(icon_X, gameObject.transform.position.y);                
            }
            else
            {
                Hearts[i].color = new Color(1f, 1f, 1f, 0f);
            }
        }
    }

    /// <summary>
    /// Initializes the health bar with a set number of sprites and relocates the bar to the specified x and y position.
    /// </summary>
    /// <param name="withNumberOfHearts">The number of icons (hearts) ti display on the health bar.</param>
    /// <param name="atPositionX">X position on the canvas.</param>
    /// <param name="atPositionY">Y position on the canvas.</param>
    internal void InitHealthBar(int withNumberOfHearts, float atPositionX, float atPositionY)
    {
        gameObject.transform.parent.transform.position = new Vector3(atPositionX, atPositionY);
        InitHealthBar(withNumberOfHearts);
    }

    /// <summary>
    /// Calling this function overrides the default sprite with the override sprite that was the Manager was initialized with.
    /// </summary>
    /// <param name="numberOfHearts">The number of icons to override.</param>
    public void OverrideSpriteFor(int numberOfHearts)
    {
        if (numberOfHearts > 0 && TotalHeartsOverridden < Capacity)
        {
            int totalHeartsToFill = TotalHeartsOverridden + numberOfHearts;
            totalHeartsToFill = totalHeartsToFill > Capacity ? Capacity : totalHeartsToFill;

            int heartsFilled = 0;
            if (IsLeftToRight)
            {
                for (int i = 0; i < Capacity && heartsFilled < numberOfHearts; i++)
                {
                    Hearts[i].overrideSprite = OverrideSprite;
                    TotalHeartsOverridden++;
                    heartsFilled++;
                }
            }
            else
            {
                for (int i = NumberOfHeartsVisible - TotalHeartsOverridden - 1; i >= 0 && heartsFilled < numberOfHearts; i--)
                {
                    Hearts[i].overrideSprite = OverrideSprite;
                    TotalHeartsOverridden++;
                    heartsFilled++;
                }
            }
        }
    }

    /// <summary>
    /// The total width of the Health Bar. (Read Only)
    /// </summary>
    public int HealthBarWidth
    {
        get
        {
            return IconWidth * 10;
        }
    }

    /// <summary>
    /// The heigh of the Health Bar. (Read Only)
    /// </summary>
    public int HealthBarHeight
    {
        get
        {
            return IconHeight;
        }
    }

    /// <summary>
    /// The Length of the entire 
    /// </summary>
    public int Capacity
    {
        get
        {
            return Hearts.Length;
        }
    }

    /// <summary>
    /// Contains the Image objects that allow access to the sprite of each unit in the health bar.
    /// </summary>
    internal Image[] Hearts;

    internal int TotalHeartsOverridden { get; private set; }
}
