﻿using System;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class IntroTriangle : MonoBehaviour
{
    // Set the variables!

    AudioSource m_MyAudioSource;
    public GameObject TriPrefab;
    public GameObject Savedialog;
    public float YSpeedMin;
    public float YSpeedMax;
    public float OpacityMin;
    public float OpacityMax;
    public float ScaleMin;
    public float ScaleMax;
    public float FinalSpeed;
    public float FinalScale;
    public float FinalOpacity;
    public float StartX;
    public float StartY;
    public float FinalZ;
    public Sprite hitcircle;
    public Sprite trianglespr;
    public Text hexcode;
    // Sliders
    public float randomadd;
    public float wait;
    public float waitamount;
    public float colors;
    public string hextext;
    public float clicked;

    public string hexcodetext;
    Collider m_Collider;

    // ET TRIGEN TRIANGLE V3

    public static byte[] ConvertHexStringToByteArray(string hexString)
    {
        if (hexString.Length % 2 != 0)
        {
            throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", hexString));
        } // detects if the hex is an odd number

        byte[] data = new byte[hexString.Length / 2];
        for (int index = 0; index < data.Length; index++)
        {
            string byteValue = hexString.Substring(index * 2, 2);
            data[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
        }

        return data;
    }


    public void Start()
    {
        byte[] colors = ConvertHexStringToByteArray("FFFFFF"); //converts hex to rgb
        randomadd = UnityEngine.Random.Range(-0.1f, 0.1f); // sets randomadd to add to value in updatecolor()
        updatecolor(); // run updatecolor to update the triangle colour at init
        SpriteRenderer spRend = transform.GetComponent<SpriteRenderer>(); //Sets the spriterendererr spRend to the SpriteRenderer component.
        Color col = spRend.color; //Sets the colour "col" to spRend.color which we set just abov ethis
        FinalOpacity = OpacityMin; // Sets FinalOpacity to the minimum opacity.
        col.a = FinalOpacity; // Sets col.a (colour alpha) to the FinalOpacity 
        spRend.color = col; // Sets spRend colour to col, while your at it why not paint yourself invisible
        StartX = UnityEngine.Random.Range(-14.1f, 14.1f); //Gets X for spawn
        StartY = -8; //Sets startY to just off screen
        //ScaleMin = 0;
        //ScaleMin = 2;
        //YSpeedMin = 1;
        //YSpeedMax = 50;

        FinalScale = UnityEngine.Random.Range(ScaleMin, ScaleMax); //Sets FinalScale to a random number between ScaleMin and ScaleMax
        FinalSpeed = UnityEngine.Random.Range(YSpeedMin, YSpeedMax); //Sets FinalSpeed to a random number between YSpeedMin and YSpeedMax

        FinalZ = FinalScale; //Sets FinalZ to Scale, so the smaller the triangles are the closer they are to the camera, closer = smaller number
        transform.position = new Vector3(StartX, StartY, FinalZ); //Sets the position to StartX and StartY, sets Z to 0
        transform.localScale = new Vector3(FinalScale, FinalScale, 1); //Sets the scale to FinalScale
        FinalSpeed = YSpeedMin; //YSpeedMin is connected to the slider in UI called "Triangle Speed". Variable YSpeedMax is unused but kept around for purposes of me being lazy.
        FinalSpeed = FinalSpeed - (FinalScale * 4); //Make it so the smaller the triangles are the faster they move by removing the size from the speed.
        if (FinalSpeed < 0.2f)
        {
            FinalSpeed = 0.2f;
        }
        // Examples
        //
        // If a triangle was the size of two, and the speed was set to 8, the speed would end out as 4.
        // Meanwhile if another triangle was set to the size of 0.2, same speed, it'd end out as 7.6
    }
    void Update()
    {
        updatecolor();
        //hexcodetext = hexcode.text;
        transform.position += Vector3.up * Time.deltaTime * FinalSpeed; //Moves up at speed of FinalSpeed
        if (transform.position.y >= 20)
        {
            Destroy(gameObject); //Destroys the gameobject if it gets too high to stop lag
        }
    }
    void updatecolor()
    {
        byte[] colors = ConvertHexStringToByteArray("FFFFFF"); // sets "colors" to the hex, converted to byte array

        // colors[0] red
        // colors[1] green
        // colors[2] blue


        Color32 c = new Color32(colors[0], colors[1], colors[2], 255); // makes new color32 and sets to the R, G, and B of the converted hex, and 255 alpha
        float h, s, v; // makes 3 floats
        Color.RGBToHSV(c, out h, out s, out v); // converts color "c" to HSV

        v += randomadd; // adds "randomadd" (randomized at init) to "v"
        v = Mathf.Clamp(v, 0.0f, 1.0f); // clamps it to 0-1, so it doesn't get too large
        Color c2 = Color.HSVToRGB(h, s, v); // converts back to RGB
        Color32 finalColor = new Color32((byte)(c2.r * 255), (byte)(c2.g * 255), (byte)(c2.b * 255), 255); // sets finalcolor to c2.r, c2.g, and c2.b timsed by 255
        GetComponent<Renderer>().material.color = finalColor; // sets triangle colour to finalcolor
    }
    // all of this stuff just sets the variables to the slider variables

    void SET_S_YSMIN(float value)
    {

        YSpeedMin = value;
    }
    void SET_S_YSMAX(float value)
    {

        YSpeedMax = value;
    }
    void SET_S_OMIN(float value)
    {

        OpacityMin = value;
    }
    void SET_S_OMAX(float value)
    {

        OpacityMax = value;
    }
    void SET_S_SMIN(float value)
    {

        ScaleMin = value;
    }
    void SET_S_SMAX(float value)
    {

        ScaleMax = value;
    }



    void OnMouseDown()
    {

    }
}
