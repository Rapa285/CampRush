using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Node
{
    private Vector3 currentPos; // Assign this in the Inspector if needed.
    private List<Node> nextDest = new List<Node>();

    public Node(Vector3 pos){
        currentPos = pos;
        nextDest = new List<Node>();
    }
    // Public getter for next destinations
    public List<Node> GetNextDestinations() => nextDest;

    // Add a connection to another node
    public void AddConnection(Node destination)
    {
        if (!nextDest.Contains(destination))
        {
            nextDest.Add(destination);
        }
    }

    // Remove a connection to another node
    public void RemoveConnection(Node destination)
    {
        if (nextDest.Contains(destination))
        {
            nextDest.Remove(destination);
        }
    }

    public void setPos(Vector3 curr_pos){
        currentPos = curr_pos;
    }

    public Vector3 getPos(){
        return currentPos;
    }

    public Node getRandomDest()
    {
        if (nextDest.Count == 0)
        {
            return null; // Return null if there are no destinations
        }

        // Calculate the total weight based on an exponential function
        float totalWeight = 0f;
        float[] weights = new float[nextDest.Count];
        for (int i = 0; i < nextDest.Count; i++)
        {
            weights[i] = Mathf.Pow(2, i); // Exponential weight: 2^i
            totalWeight += weights[i];
        }

        // Generate a random value between 0 and totalWeight
        float randomValue = Random.Range(0f, totalWeight);

        // Find the node corresponding to the random value
        float cumulativeWeight = 0f;
        for (int i = 0; i < nextDest.Count; i++)
        {
            cumulativeWeight += weights[i];
            if (randomValue < cumulativeWeight)
            {
                return nextDest[i];
            }
        }
        Debug.Log("Something went wrong!! with currentPos:"+currentPos+", nextDest.count:"+nextDest.Count+", totalWeight:"+totalWeight+", randomValue:"+randomValue+", cumulativeWeight:"+cumulativeWeight);
        return null; // This will never be reached if the logic is correct
    }

}


public class RoadGraph{
    
    private List<Node> checkpoints = new List<Node>();

    public void initCheckpoints(){
        GameObject checkpoint = GameObject.Find("Checkpoint");
        if (checkpoint != null)
        {
            // Iterate through all children of the "checkpoint" GameObject
            foreach (Transform child in checkpoint.transform)
            {
                // Add each child GameObject to the list
                Node newNode = new Node(child.position);
                checkpoints.Add(newNode);

            }

            // Optionally, print out the names of all children
            foreach (Node child in checkpoints)
            {
                Debug.Log("Checkpoint Child: " + child);
            }
        }
        else
        {
            Debug.LogError("Checkpoint GameObject not found!");
        }

        for (int i = 0; i < checkpoints.Count; i++)
        {
            for (int j = i + 1; j < checkpoints.Count; j++) // Start from i + 1 to avoid redundant comparisons
            {
                Node cp = checkpoints[i];
                Node cp2 = checkpoints[j];

                if (isParallel(cp.getPos(), cp2.getPos()))
                {
                    cp.AddConnection(cp2);
                    cp2.AddConnection(cp); // Add the reverse connection if needed
                }
            }
        }

    }

    private bool isParallel(Vector3 t1, Vector3 t2){
        if (t1.x != t2.x && t1.y != t2.y){
            return false;
        }
        return true;
    }

    public List<Node> getNodes(){
        return checkpoints;
    }

    public void removeNode(Node node){
        foreach (Node checkpoint in checkpoints){
            checkpoint.RemoveConnection(node);
        }
        checkpoints.Remove(node);
    }
}