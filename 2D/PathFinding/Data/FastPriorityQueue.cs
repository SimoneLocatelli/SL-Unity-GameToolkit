﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

/// <summary>
///     Taken from https://github.com/JamieG/AStar
///     An implementation of a min-Priority Queue using a heap.  Has O(1) .Contains()!
///     See https://github.com/BlueRaja/High-Speed-Priority-Queue-for-C-Sharp/wiki/Getting-Started for more information
/// </summary>
public sealed class FastPriorityQueue
{
    private readonly AStarNode2D[] _nodes;

    /// <summary>
    ///     Returns the number of nodes in the queue.
    ///     O(1)
    /// </summary>
    public int Count { get; private set; }

    /// <summary>
    ///     Instantiate a new Priority Queue
    /// </summary>
    /// <param name="maxNodes">The max nodes ever allowed to be enqueued (going over this will cause undefined behavior)</param>
    public FastPriorityQueue(int maxNodes)
    {
        Count = 0;
        _nodes = new AStarNode2D[maxNodes + 1];
    }

    /// <summary>
    ///     Removes every node from the queue.
    ///     O(n) (So, don't do this often!)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clear()
    {
        Array.Clear(_nodes, 1, Count);
        Count = 0;
    }

    /// <summary>
    ///     Returns (in O(1)!) whether the given node is in the queue.
    ///     If node is or has been previously added to another queue, the result is undefined unless oldQueue.ResetNode(node)
    ///     has been called
    ///     O(1)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(AStarNode2D node) => _nodes[node.QueueIndex] == node;

    /// <summary>
    ///     Removes the head of the queue and returns it.
    ///     If queue is empty, result is undefined
    ///     O(log n)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AStarNode2D Dequeue()
    {
        var returnMe = _nodes[1];

        //If the node is already the last node, we can remove it immediately
        if (Count == 1)
        {
            _nodes[1] = null;
            Count = 0;

            return returnMe;
        }

        //Swap the node with the last node
        var formerLastNode = _nodes[Count];
        _nodes[1] = formerLastNode;
        formerLastNode.QueueIndex = 1;
        _nodes[Count] = null;
        Count--;

        //Now bubble formerLastNode (which is no longer the last node) down
        CascadeDown(formerLastNode);

        return returnMe;
    }

    /// <summary>
    ///     Enqueue a node to the priority queue.  Lower values are placed in front. Ties are broken arbitrarily.
    ///     If the queue is full, the result is undefined.
    ///     If the node is already enqueued, the result is undefined.
    ///     If node is or has been previously added to another queue, the result is undefined unless oldQueue.ResetNode(node)
    ///     has been called
    ///     O(log n)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Enqueue(AStarNode2D node, double priority)
    {
        node.F = priority;
        Count++;
        _nodes[Count] = node;
        node.QueueIndex = Count;
        CascadeUp(node);
    }

    /// <summary>
    ///     Removes a node from the queue.  The node does not need to be the head of the queue.
    ///     If the node is not in the queue, the result is undefined.  If unsure, check Contains() first
    ///     O(log n)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Remove(AStarNode2D node)
    {
        //If the node is already the last node, we can remove it immediately
        if (node.QueueIndex == Count)
        {
            _nodes[Count] = null;
            Count--;

            return;
        }

        //Swap the node with the last node
        var formerLastNode = _nodes[Count];
        _nodes[node.QueueIndex] = formerLastNode;
        formerLastNode.QueueIndex = node.QueueIndex;
        _nodes[Count] = null;
        Count--;

        //Now bubble formerLastNode (which is no longer the last node) up or down as appropriate
        OnNodeUpdated(formerLastNode);
    }

    /// <summary>
    ///     This method must be called on a node every time its priority changes while it is in the queue.
    ///     <b>Forgetting to call this method will result in a corrupted queue!</b>
    ///     Calling this method on a node not in the queue results in undefined behavior
    ///     O(log n)
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void UpdatePriority(AStarNode2D node, double priority)
    {
        node.F = priority;
        OnNodeUpdated(node);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void CascadeDown(AStarNode2D node)
    {
        //aka Heapify-down
        var finalQueueIndex = node.QueueIndex;
        var childLeftIndex = 2 * finalQueueIndex;

        // If leaf node, we're done
        if (childLeftIndex > Count) return;

        // Check if the left-child is higher-priority than the current node
        var childRightIndex = childLeftIndex + 1;
        var childLeft = _nodes[childLeftIndex];

        if (HasHigherPriority(childLeft, node))
        {
            // Check if there is a right child. If not, swap and finish.
            if (childRightIndex > Count)
            {
                node.QueueIndex = childLeftIndex;
                childLeft.QueueIndex = finalQueueIndex;
                _nodes[finalQueueIndex] = childLeft;
                _nodes[childLeftIndex] = node;

                return;
            }

            // Check if the left-child is higher-priority than the right-child
            var childRight = _nodes[childRightIndex];

            if (HasHigherPriority(childLeft, childRight))
            {
                // left is highest, move it up and continue
                childLeft.QueueIndex = finalQueueIndex;
                _nodes[finalQueueIndex] = childLeft;
                finalQueueIndex = childLeftIndex;
            }
            else
            {
                // right is even higher, move it up and continue
                childRight.QueueIndex = finalQueueIndex;
                _nodes[finalQueueIndex] = childRight;
                finalQueueIndex = childRightIndex;
            }
        }
        // Not swapping with left-child, does right-child exist?
        else if (childRightIndex > Count)
        {
            return;
        }
        else
        {
            // Check if the right-child is higher-priority than the current node
            var childRight = _nodes[childRightIndex];

            if (HasHigherPriority(childRight, node))
            {
                childRight.QueueIndex = finalQueueIndex;
                _nodes[finalQueueIndex] = childRight;
                finalQueueIndex = childRightIndex;
            }
            // Neither child is higher-priority than current, so finish and stop.
            else
            {
                return;
            }
        }

        while (true)
        {
            childLeftIndex = 2 * finalQueueIndex;

            // If leaf node, we're done
            if (childLeftIndex > Count)
            {
                node.QueueIndex = finalQueueIndex;
                _nodes[finalQueueIndex] = node;

                break;
            }

            // Check if the left-child is higher-priority than the current node
            childRightIndex = childLeftIndex + 1;
            childLeft = _nodes[childLeftIndex];

            if (HasHigherPriority(childLeft, node))
            {
                // Check if there is a right child. If not, swap and finish.
                if (childRightIndex > Count)
                {
                    node.QueueIndex = childLeftIndex;
                    childLeft.QueueIndex = finalQueueIndex;
                    _nodes[finalQueueIndex] = childLeft;
                    _nodes[childLeftIndex] = node;

                    break;
                }

                // Check if the left-child is higher-priority than the right-child
                var childRight = _nodes[childRightIndex];

                if (HasHigherPriority(childLeft, childRight))
                {
                    // left is highest, move it up and continue
                    childLeft.QueueIndex = finalQueueIndex;
                    _nodes[finalQueueIndex] = childLeft;
                    finalQueueIndex = childLeftIndex;
                }
                else
                {
                    // right is even higher, move it up and continue
                    childRight.QueueIndex = finalQueueIndex;
                    _nodes[finalQueueIndex] = childRight;
                    finalQueueIndex = childRightIndex;
                }
            }
            // Not swapping with left-child, does right-child exist?
            else if (childRightIndex > Count)
            {
                node.QueueIndex = finalQueueIndex;
                _nodes[finalQueueIndex] = node;

                break;
            }
            else
            {
                // Check if the right-child is higher-priority than the current node
                var childRight = _nodes[childRightIndex];

                if (HasHigherPriority(childRight, node))
                {
                    childRight.QueueIndex = finalQueueIndex;
                    _nodes[finalQueueIndex] = childRight;
                    finalQueueIndex = childRightIndex;
                }
                // Neither child is higher-priority than current, so finish and stop.
                else
                {
                    node.QueueIndex = finalQueueIndex;
                    _nodes[finalQueueIndex] = node;

                    break;
                }
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void CascadeUp(AStarNode2D node)
    {
        //aka Heapify-up
        int parent;

        if (node.QueueIndex > 1)
        {
            parent = node.QueueIndex >> 1;
            var parentNode = _nodes[parent];

            if (HasHigherOrEqualPriority(parentNode, node))
                return;

            //Node has lower priority value, so move parent down the heap to make room
            _nodes[node.QueueIndex] = parentNode;
            parentNode.QueueIndex = node.QueueIndex;

            node.QueueIndex = parent;
        }
        else
        {
            return;
        }

        while (parent > 1)
        {
            parent >>= 1;
            var parentNode = _nodes[parent];

            if (HasHigherOrEqualPriority(parentNode, node))
                break;

            //Node has lower priority value, so move parent down the heap to make room
            _nodes[node.QueueIndex] = parentNode;
            parentNode.QueueIndex = node.QueueIndex;

            node.QueueIndex = parent;
        }

        _nodes[node.QueueIndex] = node;
    }

    /// <summary>
    ///     Returns true if 'higher' has higher priority than 'lower', false otherwise.
    ///     Note that calling HasHigherOrEqualPriority(node, node) (ie. both arguments the same node) will return true
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool HasHigherOrEqualPriority(AStarNode2D higher, AStarNode2D lower) => higher.F <= lower.F;

    /// <summary>
    ///     Returns true if 'higher' has higher priority than 'lower', false otherwise.
    ///     Note that calling HasHigherPriority(node, node) (ie. both arguments the same node) will return false
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool HasHigherPriority(AStarNode2D higher, AStarNode2D lower) => higher.F < lower.F;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void OnNodeUpdated(AStarNode2D node)
    {
        //Bubble the updated node up or down as appropriate
        var parentIndex = node.QueueIndex >> 1;

        if (parentIndex > 0 && HasHigherPriority(node, _nodes[parentIndex]))
            CascadeUp(node);
        else
            CascadeDown(node);
    }


#if DEBUG

    /// <summary>
    /// Only for debugging purposes
    /// </summary>
    public List<AStarNode2D> ToList()
    {
        var nodesList = new List<AStarNode2D>();

        foreach(var node in _nodes)
        {
            if (node == null)
                continue;

            nodesList.Add(node);
        }

        return nodesList;
    }

#endif
}