using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponIcons : MonoBehaviour {

	// Dit script word vastgeplakt aan een leeg gameobject waardoor de webicons kunnen wisselen.
	Image image;

	public Sprite pistolSprite;
	public Sprite machinegunSprite;
	public Sprite shotgunSprite;

	void Start () {
		image = GetComponent<Image>();
	}


	void Update () {

		if (Gun.gunType == Gun.GunType.Pistol) {
			image.sprite = pistolSprite;
		} else if (Gun.gunType == Gun.GunType.Shotgun) {
			image.sprite = shotgunSprite;
		} else if (Gun.gunType == Gun.GunType.Machinegun) {
			image.sprite = machinegunSprite;
		}
	}
}
