using System;

namespace AlfecheExer2
{

    #region Binary Search Tree Program
    class Program
    {
        static void Main(string[] args)
        {
            #region Initialization of the Tree
            ///An array is used to output the menu and possible choices of the tree
            string[] menu = new string[10];
            menu[0] = "Binary Search Tree";
            menu[1] = "[1] Insert Node to the Tree";
            menu[2] = "[2] Delete Node from the Tree";
            menu[3] = "[3] Minimum Node";
            menu[4] = "[4] Maximum Node";
            menu[5] = "[5] Successor of the Node";
            menu[6] = "[6] Predecessor of the Node";
            menu[7] = "[7] Search Node";
            menu[8] = "[8] Print the Tree";
            menu[9] = "[9] Exit";

            ///A loop is used to iterate the printing of the menu items
            foreach (string i in menu)
            {
                Console.WriteLine(i);
            }

            ///The tree is initialized at the instance of the form
            Tree localtree = new Tree();
            ///The menu choice variable is declared which will be reused in the implementation of the action buttons
            int menu_choice;
            #endregion

            #region Console Program
            do
            {
                Console.Write("\nSelect an action: ");
                menu_choice = Convert.ToInt32(Console.ReadLine());
                switch (menu_choice)
                {
                    case 1:
                        Console.Write("Enter the node value of your choice: ");
                        int key_insert = Convert.ToInt32(Console.ReadLine());
                        localtree.insert(key_insert);
                        break;
                    case 2:
                        Console.Write("Enter the node value to be deleted: ");
                        int key_delete = Convert.ToInt32(Console.ReadLine());
                        localtree.delete(key_delete);
                        break;
                    case 3:
                        localtree.min();
                        break;
                    case 4:
                        localtree.max();
                        break;
                    case 5:
                        Console.Write("Enter the node value of your choice: ");
                        int key_suc = Convert.ToInt32(Console.ReadLine());
                        localtree.successor(key_suc);
                        break;
                    case 6:
                        Console.Write("Enter the node value of your choice: ");
                        int key_pred = Convert.ToInt32(Console.ReadLine());
                        localtree.predecessor(key_pred);
                        break;
                    case 7:
                        Console.Write("Enter the node value of your choice: ");
                        int key_search = Convert.ToInt32(Console.ReadLine());
                        localtree.search(key_search);
                        break;
                    case 8:
                        Console.WriteLine("The nodes of the tree are as follows: ");
                        localtree.inorder();
                        Console.WriteLine("");
                        break;
                }
            }
            while (menu_choice != 9 || menu_choice > 9); // exit criteria for the menu.

            #endregion

        }
    }
    #endregion
    #region Node and Tree Classes
    /// <summary>
    /// The tree is initialized as class with attribues of root and nodes.
    /// The methods within the tree includes: insert, delete, maximum, minimum, successor, predecessor, and an inorder travesal.
    /// The foundational feature of a BST is that values are evaluated against a root/parent leaf.
    /// Values lesser than the root goes to its left node and values greater than goes to the right.
    /// </summary>

    public class Tree
    {
        /// <summary>
        /// The nodes are implemented as a class with attributes of item and the pointers to left and right.
        /// </summary>
        class Node
        {
            public int item;
            public Node left;
            public Node right;
            public Node(int value) // Class constructor | sets the default value of the object from this class
            {
                item = value;
                left = right = null;
            }
        }

        Node root; // field for root
        static Node suc; // field for successor
        static Node pred; // field for predecessor

        public Tree() // Class constructor of the tree with the root initially set to null.
        {
            root = null;
        }

        #region Main Methods of the Tree

        /// <summary>
        /// This method takes in two parameters, the root field and the key to be entered.
        /// When the tree is empty, the !first! key entered becomes the root and the reference point to all succeeding nodes.
        /// The method evaluates all succeeding nodes after and recursively adds the nodes to their respective left/right subtrees.
        /// </summary>


        Node insertRec(Node root, int key)
        {
            if (root == null)
            {
                return (new Node(key));
            }

            else
            {
                if (key <= root.item)
                {
                    root.left = insertRec(root.left, key);
                }

                else
                {
                    root.right = insertRec(root.right, key);
                }

                return root;
            }
        }

        /// <summary>
        /// The delete method takes in two parameters, the root and the key of the node to be deleted.
        /// 
        /// </summary>


        Node deleteRec(Node root, int key)
        {
            if (root == null)
            {
                return null;
            }

            if (key < root.item)
            {
                root.left = deleteRec(root.left, key);
            }

            else if (key > root.item)
            {
                root.right = deleteRec(root.right, key);
            }

            else
            {
                if (root.left == null)
                {
                    return root.right;
                }

                else if (root.right == null)
                {
                    return root.left;
                }

                root.item = minvalue(root.right);
                root.right = deleteRec(root.right, key);
            }
            return root;
        }

        /// <summary>
        /// This method allows us to print the entirety of the tree in an inorder traversal.
        /// This starts with the leftmost leaf ascending to the righmost leaf of the tree.
        /// This means that the root is at the "center" of the traversal.
        /// </summary>

        void inorderRec(Node root)
        {
            if (root != null)
            {
                inorderRec(root.left);
                Console.Write(root.item + " ");
                inorderRec(root.right);
            }
        }

        /// <summary>
        /// This method traverses the leftmost leaf of the tree which represents the lowest value of the tree.
        /// This takes in the root as the sole parameter.
        /// </summary>

        int minvalue(Node root)
        {
            int current = root.item;

            while (root.left != null)
            {
                current = root.left.item;
                root = root.left;
            }

            return (current);
        }

        /// <summary>
        /// This method traverses the rightmost leaf of the tree which represents the highest value of the tree.
        /// This takes in the root as the sole parameter.
        /// </summary>

        int maxvalue(Node root)
        {
            int current = root.item;

            while (root.right != null)
            {
                current = root.right.item;
                root = root.right;
            }

            return (current);
        }

        /// <summary>
        /// This method is a boolean which searches and returns whether a node already exists within the tree or not.
        /// This takes in the root and the key to be searched as parameters.
        /// This also 
        /// </summary>

        bool searchNode(Node node, int key)
        {
            if (node == null)
            {
                return false;
            }
            if (node.item == key)
            {
                return true;
            }

            bool res_left = searchNode(node.left, key);
            if (res_left) return true;

            bool res_right = searchNode(node.right, key);
            return res_right;
        }

        /// <summary>
        /// This method locates the inorder successor and predecessor of a given key.
        /// This uses also initialized fields within the Tree class.
        /// The method searches for the given key within the tree and evaluates for the successor and predecessor of the given key.
        /// The minimum value of the right subtree is the the successor and the maximum value of the left subtree is the predecessor.
        /// This method also accounts if the key is greater or lesser than root. 
        /// </summary>


        static void succpred(Node node, int key)
        {
            if (node == null)
            {
                return;
            }

            while (node != null)
            {
                if (node.item == key)
                {

                    if (node.right != null)
                    {
                        suc = node.right;
                        while (suc.left != null)
                            suc = suc.left;
                    }
                    if (node.left != null)
                    {
                        pred = node.left;
                        while (pred.right != null)
                            pred = pred.right;
                    }
                    return;
                }

                else if (node.item < key)
                {
                    pred = node;
                    node = node.right;
                }
                else
                {
                    suc = node;
                    node = node.left;
                }
            }

        }

        #endregion

        #region Helper Methods

        //Helper method for insert.
        //Automatically assigns the root as a parameter to only allow keys as input in the driver program.
        public void insert(int key)
        {
            root = insertRec(root, key);
        }

        //Helper method for delete.
        //Automatically assigns the root as a parameter to only allow keys as input in the driver program.
        public void delete(int key)
        {
            root = deleteRec(root, key);
        }

        //Helper method for inorder traversal.
        //Automatically assigns the root as a parameter.
        public void inorder()
        {
            inorderRec(root);
        }

        //Helper method for minimum value.
        //Automatically assigns the root as a parameter.
        public void min()
        {
            Console.WriteLine("\nThe minimum value of the tree is " + minvalue(root));
        }

        //Helper method for maximum value.
        //Automatically assigns the root as a parameter.
        public void max()
        {
            Console.WriteLine("The maximum value of the tree is " + maxvalue(root));
        }

        //Helper method for search.
        //Automatically assigns the root as a parameter to only allow keys as input in the driver program.
        public void search(int key)
        {
            if (searchNode(root, key))
            {
                Console.WriteLine("\nThe item is in the tree.");
            }
            else
            {
                Console.WriteLine("\nThe item is not in the tree.");
            }
        }

        //Helper method for successor.
        //Automatically assigns the root as a parameter to only allow keys as input in the driver program.
        public void successor(int key)
        {
            succpred(root, key);
            Console.WriteLine("Successor is " + suc.item);
        }

        //Helper method for predecessor.
        //Automatically assigns the root as a parameter to only allow keys as input in the driver program.
        public void predecessor(int key)
        {
            succpred(root, key);
            Console.WriteLine("Predecessor is " + pred.item);
        }

        #endregion
    }
    #endregion
}