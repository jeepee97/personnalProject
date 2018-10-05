using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


// classe generique utiliser pour augmenter la rapiditer.
public class Heap<T> where T : IHeapItem<T> {

    T[] items_;
    int currentItemCount_;

    public Heap(int maxHeapSize)
    {
        items_ = new T[maxHeapSize];
    }

    public void Add(T item)
    {
        item.heapIndex = currentItemCount_;
        items_[currentItemCount_] = item;
        sortUp(item);
        currentItemCount_++;
    }

    public T removeFirst()
    {
        T firstItem = items_[0];
        currentItemCount_--;
        items_[0] = items_[currentItemCount_];
        items_[0].heapIndex = 0;
        sortDown(items_[0]);
        return firstItem;
    }

    public void updateItem(T item)
    {
        sortUp(item);
    }

    public int Count
    {
        get
        {
            return currentItemCount_;
        }
    }

    public bool Contains(T item)
    {
        return Equals(items_[item.heapIndex], item);
    }
    void sortDown(T item)
    {
        while (true)
        {
            int childIndexLeft = item.heapIndex * 2 + 1;
            int childIndexRight = item.heapIndex * 2 + 2;
            int swapIndex = 0;

            if (childIndexLeft < currentItemCount_)
            {
                swapIndex = childIndexLeft;
                if (childIndexRight < currentItemCount_)
                {
                    if (items_[childIndexLeft].CompareTo(items_[childIndexRight]) < 0)
                    {
                        swapIndex = childIndexRight;
                    }
                }

                if (item.CompareTo(items_[swapIndex]) < 0)
                {
                    swap(item, items_[swapIndex]);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }
    void sortUp(T item)
    {
        int parentIndex = (item.heapIndex - 1) / 2;
        while (true)
        {
            T parentItem = items_[parentIndex];
            if (item.CompareTo(parentItem) > 0)
            {
                swap(item, parentItem);
            }
            else
            {
                break;
            }

            parentIndex = (item.heapIndex - 1) / 2;
        }
    }

    void swap(T itemA, T itemB)
    {
        items_[itemA.heapIndex] = itemB;
        items_[itemB.heapIndex] = itemA;
        int itemAIndex = itemA.heapIndex;
        itemA.heapIndex = itemB.heapIndex;
        itemB.heapIndex = itemAIndex;
    }
}

public interface IHeapItem<T> : IComparable<T>
{
    int heapIndex
    {
        get;
        set;
    }
}
