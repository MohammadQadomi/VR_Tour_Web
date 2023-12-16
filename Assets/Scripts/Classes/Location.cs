using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Locations
{
    public List<Location> locations = new List<Location>();

    public bool Add(Location location)
    {
        var index = Contains(location);
        if (index == -1)
        {
            locations.Add(location);
            return true;
        }
        else
        {
            locations[index] = location;
            return false;
        }
    }

    public int Contains(Location location)
    {
        for (int i = 0; i < locations.Count; i++)
        {
            if (locations[i].id == location.id) return i;
        }
        return -1;
    }

    public Location Find(int id)
    {
        foreach (var item in locations)
        {
            if (item.id == id) return item;
        }
        return null;
    }
}

public class Location
{
    public int id = 0;
    public string imagePath = "";
    public string voiceOverPath = "";
    public List<Arrow> arrows;
    public List<Hotspot> hotspots;
    public List<Desk> desks;


    public Location(int id, string imagePath, string voiceOverPath, List<Arrow> arrows, List<Hotspot> hotspots, List<Desk> desks)
    {
        this.id = id;
        this.imagePath = imagePath;
        this.voiceOverPath = voiceOverPath;
        this.arrows = arrows;
        this.hotspots = hotspots;
        this.desks = desks;
    }
}

public class Arrow
{
    public string text = "";

    public float positionX = 0;
    public float positionY = 0;
    public float positionZ = 0;

    public float rotationX = 0;
    public float rotationY = 0;
    public float rotationZ = 0;
    public float rotationW = 0;

    public float scaleX = 1;
    public float scaleY = 1;
    public float scaleZ = 1;

    public int destinationId;

    public bool isHidden = false;

    public Arrow(string text, Vector3 position, Quaternion rotation, Vector3 scale,int destinationId, bool isHidden)
    {
        this.text = text;

        this.positionX = position.x;
        this.positionY = position.y;
        this.positionZ = position.z;

        this.rotationX = rotation.x;
        this.rotationY = rotation.y;
        this.rotationZ = rotation.z;
        this.rotationW = rotation.w;

        this.scaleX = scale.x;
        this.scaleY = scale.y;
        this.scaleZ = scale.z;

        this.destinationId = destinationId;

        this.isHidden = isHidden;
    }
}

public class Hotspot
{
    public string text = "";
    public float positionX = 0;
    public float positionY = 0;
    public float positionZ = 0;

    public float rotationX = 0;
    public float rotationY = 0;
    public float rotationZ = 0;
    public float rotationW = 0;

    public float scaleX = 1;
    public float scaleY = 1;
    public float scaleZ = 1;

    public bool isHidden = false;

    public Hotspot(string text, Vector3 position, Quaternion rotation, Vector3 scale, bool isHidden)
    {
        this.text = text;
        this.positionX = position.x;
        this.positionY = position.y;
        this.positionZ = position.z;

        this.rotationX = rotation.x;
        this.rotationY = rotation.y;
        this.rotationZ = rotation.z;
        this.rotationW = rotation.w;

        this.scaleX = scale.x;
        this.scaleY = scale.y;
        this.scaleZ = scale.z;

        this.isHidden = isHidden;
    }
}


public class Desk
{
    public int id;
    public string employeeName;
    public string employeeDepartment;
    public string employeeDescription;

    public float positionX = 0;
    public float positionY = 0;
    public float positionZ = 0;

    public float rotationX = 0;
    public float rotationY = 0;
    public float rotationZ = 0;
    public float rotationW = 0;

    public float scaleX = 1;
    public float scaleY = 1;
    public float scaleZ = 1;

    public bool isHidden = false;

    public Desk(string employeeName, Vector3 position, Quaternion rotation, Vector3 scale, bool isHidden,string employeeDepartment="", string employeeDescription = "")
    {
        this.employeeName = employeeName;

        this.positionX = position.x;
        this.positionY = position.y;
        this.positionZ = position.z;

        this.rotationX = rotation.x;
        this.rotationY = rotation.y;
        this.rotationZ = rotation.z;
        this.rotationW = rotation.w;

        this.scaleX = scale.x;
        this.scaleY = scale.y;
        this.scaleZ = scale.z;

        this.isHidden = isHidden;

        this.employeeDepartment = employeeDepartment;
        this.employeeDescription = employeeDescription;
    }
}