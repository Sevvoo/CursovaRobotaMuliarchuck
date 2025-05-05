using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackQueueAppWinForms;

namespace MyListTests
{
    [TestClass]
    public class MyListTests
    {
        [TestMethod]
        public void EmptyList_ToList_ShouldReturnEmptyCollection()
        {
            // Arrange
            var list = new MyList();

            // Act
            var result = list.ToList();

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void AddFirst_EmptyList_ShouldAddSingleElement()
        {
            // Arrange
            var list = new MyList();

            // Act
            list.AddFirst(10);
            var result = list.ToList();

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(10, result[0]);
        }

        [TestMethod]
        public void AddFirst_ExistingList_ShouldAddElementAtBeginning()
        {
            // Arrange
            var list = new MyList();
            list.AddFirst(20);

            // Act
            list.AddFirst(10);
            var result = list.ToList();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(10, result[0]);
            Assert.AreEqual(20, result[1]);
        }

        [TestMethod]
        public void AddLast_EmptyList_ShouldAddSingleElement()
        {
            // Arrange
            var list = new MyList();

            // Act
            list.AddLast(10);
            var result = list.ToList();

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(10, result[0]);
        }

        [TestMethod]
        public void AddLast_ExistingList_ShouldAddElementAtEnd()
        {
            // Arrange
            var list = new MyList();
            list.AddLast(10);

            // Act
            list.AddLast(20);
            var result = list.ToList();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(10, result[0]);
            Assert.AreEqual(20, result[1]);
        }

        [TestMethod]
        public void RemoveAll_ElementExists_ShouldRemoveElement()
        {
            // Arrange
            var list = new MyList();
            list.AddLast(10);
            list.AddLast(20);
            list.AddLast(10);

            // Act
            bool result = list.RemoveAll(10);
            var items = list.ToList();

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, items.Count);
            Assert.AreEqual(20, items[0]);
        }

        [TestMethod]
        public void RemoveAll_ElementDoesNotExist_ShouldReturnFalse()
        {
            // Arrange
            var list = new MyList();
            list.AddLast(10);
            list.AddLast(20);

            // Act
            bool result = list.RemoveAll(30);
            var items = list.ToList();

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(2, items.Count);
        }

        [TestMethod]
        public void RemoveAll_EmptyList_ShouldReturnFalse()
        {
            // Arrange
            var list = new MyList();

            // Act
            bool result = list.RemoveAll(10);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RemoveAll_RemovingOnlyElement_ShouldLeaveEmptyList()
        {
            // Arrange
            var list = new MyList();
            list.AddLast(10);

            // Act
            list.RemoveAll(10);
            var result = list.ToList();

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Clear_PopulatedList_ShouldEmptyList()
        {
            // Arrange
            var list = new MyList();
            list.AddLast(10);
            list.AddLast(20);

            // Act
            list.Clear();
            var result = list.ToList();

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void InsertAfter_TargetExists_ShouldInsertElement()
        {
            // Arrange
            var list = new MyList();
            list.AddLast(10);
            list.AddLast(30);

            // Act
            bool result = list.InsertAfter(10, 20);
            var items = list.ToList();

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(3, items.Count);
            Assert.AreEqual(10, items[0]);
            Assert.AreEqual(20, items[1]);
            Assert.AreEqual(30, items[2]);
        }

        [TestMethod]
        public void InsertAfter_TargetDoesNotExist_ShouldReturnFalse()
        {
            // Arrange
            var list = new MyList();
            list.AddLast(10);
            list.AddLast(30);

            // Act
            bool result = list.InsertAfter(20, 40);
            var items = list.ToList();

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(2, items.Count);
        }

        [TestMethod]
        public void InsertAfter_EmptyList_ShouldReturnFalse()
        {
            // Arrange
            var list = new MyList();

            // Act
            bool result = list.InsertAfter(10, 20);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void InsertAfter_LastElement_ShouldInsertAtEnd()
        {
            // Arrange
            var list = new MyList();
            list.AddLast(10);

            // Act
            list.InsertAfter(10, 20);
            var items = list.ToList();

            // Assert
            Assert.AreEqual(2, items.Count);
            Assert.AreEqual(10, items[0]);
            Assert.AreEqual(20, items[1]);
        }

        [TestMethod]
        public void InsertBefore_TargetExists_ShouldInsertElement()
        {
            // Arrange
            var list = new MyList();
            list.AddLast(10);
            list.AddLast(30);

            // Act
            bool result = list.InsertBefore(30, 20);
            var items = list.ToList();

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(3, items.Count);
            Assert.AreEqual(10, items[0]);
            Assert.AreEqual(20, items[1]);
            Assert.AreEqual(30, items[2]);
        }

        [TestMethod]
        public void InsertBefore_TargetDoesNotExist_ShouldReturnFalse()
        {
            // Arrange
            var list = new MyList();
            list.AddLast(10);
            list.AddLast(30);

            // Act
            bool result = list.InsertBefore(20, 40);
            var items = list.ToList();

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(2, items.Count);
        }

        [TestMethod]
        public void InsertBefore_EmptyList_ShouldReturnFalse()
        {
            // Arrange
            var list = new MyList();

            // Act
            bool result = list.InsertBefore(10, 20);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void InsertBefore_FirstElement_ShouldInsertAtBeginning()
        {
            // Arrange
            var list = new MyList();
            list.AddLast(20);

            // Act
            list.InsertBefore(20, 10);
            var items = list.ToList();

            // Assert
            Assert.AreEqual(2, items.Count);
            Assert.AreEqual(10, items[0]);
            Assert.AreEqual(20, items[1]);
        }
    }
}