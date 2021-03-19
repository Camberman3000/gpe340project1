using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;
using UnityEngine.UI;

[System.Serializable]
public class WeightedObject : MonoBehaviour
{

    [Header("Item Drop Settings")]
    public ItemDrops[] itemDrops; // List of items that can drop
    public double itemDropChance = 0.5f;
    public double[] cdfArray;

    // Start is called before the first frame update
    void Start()
    {
        SetCDFArray();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Object SelectItemToDrop()
    {
        System.Random rnd = new System.Random(); // Get a random double
        double rand = rnd.NextDouble(); // Get the next random double in sequence
        int index = System.Array.BinarySearch(cdfArray, rand * cdfArray.Last()); // Get a random value for index
        if (index < 0) // Check if index is negative
            index = ~index; // If negative then use bitwise 
        return itemDrops[index].GetValue(); // Return object
    }

    // Create the cumulative distribution function array
    public void SetCDFArray()
    {
        cdfArray = new double[itemDrops.Length]; // Create a new double array of itemDrops length
        cdfArray[0] = itemDrops[0].GetChance(); // Set the first CDF to the first itemDrops value

        for (int i = 1; i < cdfArray.Length; i++) // Loop through the rest of the CDF array
        {
            cdfArray[i] = cdfArray[i - 1] + itemDrops[i].GetChance(); // Set the CDF array to cumulative value of all previous indices
        }
    }
    // Drop an item OnDie
    public void DropItem()
    {
        //Debug.Log("Drop item");
        if (Random.Range(0f, 1f) < itemDropChance) // Chance an item will drop
        {
            Transform tf = this.transform;            
            Instantiate(SelectItemToDrop(), tf.position + Vector3.up, Quaternion.identity); // Instantiate the selected item
            Debug.Log("Dropped");
        }
    }
}