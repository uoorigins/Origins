//#define BULLETIN_DEBUG 
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
    #region Bulletin Board Item
    [FlipableAttribute(0x1E5E, 0x1E5F)]
    public class BulletinBoard : Item
    {
        [CommandProperty(AccessLevel.GameMaster)]
        public bool RemoveAllPosts
        {
            get { return false; }
            set
            {
                if (value == true)
                {
                    Console.WriteLine("Removing all posts on bulletin board '{0}' {1}", this.Name, this.Serial);
                    int BasePostCount = this.Items.Count;
                    this.Items.Clear();
                    Console.WriteLine("All posts removed from bulletin board {0} {1}. Removed a total of {3} post(s)", this.Name, this.Serial, BasePostCount);
                }
            }
        }

        public override bool Decays
        {
            get { return false; }
        }

        [Constructable]
        public BulletinBoard()
            : base(0x1E5E)
        {
            this.Movable = false;
            this.Name = "Bounty Board";
        }

        public BulletinBoard(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override void OnSingleClick(Mobile from)
        {
            List<Item> list = new List<Item>();

            list.Clear();

            foreach (Item item in World.Items.Values)
            {
                if (item is BulletinBoardPost)
                    list.Add(item);
            }

            from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", String.Format("a bounty board with {0} posted bounties", list.Count)));
        }

        public override void OnDoubleClick(Mobile from)
        {
            // Make sure the character is within range to open the board 
            if (from.InRange(this.GetWorldLocation(), 2) == true)
            {
                NetState state = from.NetState;

                // Open the bulletin board 
                    from.Send(new BulletinBoardOpenPacket(this));

                // Send the list of items 
                    if (state.ContainerGridLines)
                        from.Send(new BulletinBoardFillItemsPacket(this));
                    else
                        from.Send(new BulletinBoardFillItemsPacket6(this));
            }
            else
                from.LocalOverheadMessage(MessageType.Regular, 0x3B2, true, "I can't reach that."); // I can't reach that.
        }
    }
    #endregion

    #region Bulletin Board Global Post List
    internal class BulletinBoardGlobalPostList
    {
        /// <summary> 
        /// Contains the list of global post items. 
        /// </summary> 
        internal static List<Item> m_GlobalPostList = new List<Item>();
        //internal static ArrayList m_GlobalPostList = new ArrayList();

        /// <summary> 
        /// Loads the global post list with all global posts in the world. 
        /// </summary> 
        public static void Initialize()
        {
            // Find each global post in the world 
            foreach (Item item in World.Items.Values)
            {
                // Add each global post to the global post list 
                if (item is BulletinBoardGlobalPost)
                    m_GlobalPostList.Add(item);
            }
        }
    }
    #endregion

    #region Bulletin Board Global Post Item
    public class BulletinBoardGlobalPost : BulletinBoardPost
    {
        [Constructable]
        public BulletinBoardGlobalPost(string Subject, string Author, string[] Message) :
            base(Subject, Author, Message)
        {
        }

        public BulletinBoardGlobalPost(Serial serial)
            : base(serial)
        {
        }

        public static bool AddGlobalPost(BulletinBoardGlobalPost GlobalPost)
        {
            BulletinBoardGlobalPostList.m_GlobalPostList.Add(GlobalPost);
            return true;
        }

        public static BulletinBoardGlobalPost AddGlobalPost(string Subject, string Author, string[] Message)
        {
            BulletinBoardGlobalPost GlobalPost = new BulletinBoardGlobalPost(Subject, Author, Message);
            BulletinBoardGlobalPostList.m_GlobalPostList.Add(GlobalPost);
            return GlobalPost;
        }

        public static bool RemoveGlobalPost(BulletinBoardGlobalPost GlobalPost)
        {
            BulletinBoardGlobalPostList.m_GlobalPostList.Remove(GlobalPost);
            return true;
        }

        public override void OnDelete()
        {
            // If the item is deleted, remove the post from the global list 
            BulletinBoardGlobalPostList.m_GlobalPostList.Remove(this);
            base.OnDelete();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
        }
    }
    #endregion

    #region Bulletin Board Post Item
    public class BulletinBoardPost : Item
    {
        private string _Subject = string.Empty;
        private string _Author = string.Empty;
        private string _Date = string.Empty;
        private string[] _Message = new string[0];

        public string Subject
        {
            get { return this._Subject; }
            set { this._Subject = value; }
        }

        public string Author
        {
            get { return this._Author; }
        }

        public string Date
        {
            get { return this._Date; }
        }

        public string[] Message
        {
            get { return this._Message; }
            set { this._Message = value; }
        }

        [Constructable]
        public BulletinBoardPost()
        {
            this.Movable = false;
        }

        [Constructable]
        public BulletinBoardPost(string Subject, string Author, string[] Message)
        {
            this.Movable = false;
            DateTime PostTime = DateTime.Now;
            this._Subject = Subject;
            this._Author = Author;
            this._Date = "Day " + PostTime.DayOfYear + " @ " + PostTime.ToString("t");
            this._Message = Message;
        }

        public BulletinBoardPost(Serial serial)
            : base(serial)
        {
        }

        public override void OnParentDeleted(object Parent)
        {
            // Delete the post if the parent object is deleted 
            if (this.Deleted == false)
                this.Delete();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);

            writer.Write(this._Subject);
            writer.Write(this._Author);
            writer.Write(this._Date);
            writer.Write(this._Message.Length);
            for (int x = 0; x < this._Message.Length; x++)
                writer.Write(this._Message[x]);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        this._Subject = reader.ReadString();
                        this._Author = reader.ReadString();
                        this._Date = reader.ReadString();

                        int MessageLength = reader.ReadInt();
                        this._Message = new string[MessageLength];
                        for (int x = 0; x < this._Message.Length; x++)
                            this._Message[x] = reader.ReadString();

                        break;
                    }
            }
        }

        public static void Initialize()
        {
            // Register the packet handler for packet type 0x71 
            PacketHandlers.Register(0x71, 0, true, new OnPacketReceive(BulletinBoardMessage));
        }

        public static void BulletinBoardMessage(NetState state, PacketReader pvSrc)
        {
            try
            {
                // Log the incoming packet 
                BulletinPacket.LogPacket("BulletinSendPostMessage", pvSrc.Buffer);

                // Get the player character who performed the action that sent the packet 
                Mobile from = state.Mobile;

#if BULLETIN_DEBUG 
                // Log to the console information about the selected posting 
                Console.WriteLine( "BulletinSendPostMessage: Post Requested from Mobile '{0}' {1}", from.Name, from.Serial ); 
#endif

                // Get the type of bulletin message (packet sub type) 
                BulletinPacket.PacketSubType SubType = (BulletinPacket.PacketSubType)pvSrc.ReadByte();
#if BULLETIN_DEBUG 
                Console.WriteLine( "BulletinSendPostMessage: Sub-Type={0}", SubType.ToString() ); 
#endif

                // Find the bulletin board this packet is referring to 
                BulletinBoard Board = World.FindItem(pvSrc.ReadInt32()) as BulletinBoard;
                if (Board == null)
                    return;

#if BULLETIN_DEBUG 
                // Log to the console information about the selected posting 
                Console.WriteLine( "BulletinSendPostMessage: Post Requested from Bulletin Board '{0}' {1} - Total Items = {2}", Board.Name, Board.Serial, Board.Items.Count ); 
#endif

                // Switch the message sub-type 
                switch (SubType)
                {
                    // Client wants to post a new message 
                    case BulletinPacket.PacketSubType.RequestPostCreation:
                        {
                            from.SendAsciiMessage("You cannot post messages on this board.");
                            return;
                            // Make sure the client is still close enough to interact with the bulletin board 
                            if (!from.InRange(Board.GetWorldLocation(), 2))
                            {
                                from.SendMessage("You are too far away from the board to post a message.");
                                return;
                            }

                            // Read in the next 4 bytes to see if this is a reply 
                            BulletinBoardPost ReplyToPost = World.FindItem(pvSrc.ReadInt32()) as BulletinBoardPost;

                            // Check if the reply is to a global posting 
                            if (ReplyToPost is BulletinBoardGlobalPost)
                            {
                                // Tell the player that they can not reply to this message 
                                state.Mobile.SendMessage("You may not reply to this message.");
                                return;
                            }

                            // Get the length of the subject 
                            short SubjectLength = pvSrc.ReadByte();

                            // Get the subject line including the terminatin NULL 
                            string Subject = pvSrc.ReadString();

                            // Get the number of message lines 
                            short MessageLines = pvSrc.ReadByte();

                            // Retrieve all of the lines in the message 
                            string[] Message = new string[MessageLines];
                            for (int x = 0; x < Message.Length; x++)
                            {
                                short CurrentLineLength = pvSrc.ReadByte();
                                Message[x] = pvSrc.ReadString();
                            }

                            // Now that all of the data has been collected, create a BulletinBoardPost item 
                            BulletinBoardPost NewPost = new BulletinBoardPost(Subject, from.Name, Message);

                            // Check if this is a reply to a previous post 
                            if (ReplyToPost != null)
                            {
                                // Check if the post being replied to is a base post 
                                // on the board.  Only base posts can be replied to. 
                                // This code ensures that replies can only be done to 
                                // base posts, and not to other replies 
                                if (ReplyToPost.Parent == Board)
                                    ReplyToPost.AddItem(NewPost);
                                else
                                    ((Item)(ReplyToPost.Parent)).AddItem(NewPost);

                                // Send update to the client that a reply was posted 
                                // This shows the new reply immediately on the board 
                                // (Strange, but the client automatically does this part 
                                //  if the posting is not a reply) 
                                from.Send(new AddPostReplyItemPacket(NewPost));
                            }
                            else
                            {
                                // This is a new post, so add it to the boards item list 
                                Board.AddItem(NewPost);
                            }

#if BULLETIN_DEBUG 
                        // Log to the console information about the selected posting 
                        Console.WriteLine( "BulletinSendPostMessage: Added New Post {0} to Bulletin Board '{1}' {2} - Total Items = {3}", NewPost.Serial, Board.Name, Board.Serial, Board.Items.Count ); 
#endif
                            break;
                        }

                    // Client is requesting a summary of a posted message 
                    case BulletinPacket.PacketSubType.RequestPostSummary:
                        {
                            // Try to find the post that this message is referring to 
                            int PostSerial = pvSrc.ReadInt32();
                            BulletinBoardPost Post = World.FindItem(PostSerial) as BulletinBoardPost;
                            if (Post == null)
                            {
                                Console.WriteLine("Unknown Bulletin Board Post Item - Serial: {0}", PostSerial);
                                return;
                            }

                            from.Send(new BulletinBoardSendPostSummaryPacket(Board, Post));
                            break;
                        }

                    // Client is requesting the full details of the post (the entire message) 
                    case BulletinPacket.PacketSubType.RequestPostMessage:
                        {
                            // Make sure the client is still close enough to interact with the bulletin board 
                            if (!from.InRange(Board.GetWorldLocation(), 2))
                            {
                                from.SendMessage("You are too far away from the board to read the message.");
                                return;
                            }

                            // Try to find the post that this message is referring to 
                            int PostSerial = pvSrc.ReadInt32();
                            BulletinBoardPost Post = World.FindItem(PostSerial) as BulletinBoardPost;
                            if (Post == null)
                            {
                                Console.WriteLine("Unknown Bulletin Board Post Item - Serial: {0}", PostSerial);
                                return;
                            }

                            from.Send(new BulletinBoardPostPacket(Post));
                            break;
                        }

                    // Client is requesting to remove a post 
                    case BulletinPacket.PacketSubType.RequestPostRemove:
                        {
                            // Don't handle this situation at the moment 
                            // The T2A client and the UOTD client behave differently 
                            // so therefore, it's not handled at all for simplicity 
                            /* 
                            // Try to find the post that this message is referring to 
                            int PostSerial = pvSrc.ReadInt32(); 
                            BulletinBoardPost Post = World.FindItem( PostSerial ) as BulletinBoardPost; 
                            if ( Post == null ) 
                            { 
                                Console.WriteLine( "Unknown Bulletin Board Post Item - Serial: {0}", PostSerial ); 
                                return; 
                            } 

                            // Delete the post item 
                            Post.Delete(); 
                            */
                            break;
                        }

                    default:
                        {
                            Console.WriteLine("BulletinBoardPost: Unknown Bulletin Board Message SubType: ", SubType);
                            break;
                        }
                }
            }
            catch (System.Exception se)
            {
                Console.WriteLine(se.ToString());
            }

            return;
        }
    }
    #endregion

    #region Bulletin Board Packet Abstract Class
    public abstract class BulletinPacket : Packet
    {
        public BulletinPacket(int packetID)
            : base(packetID)
        {
        }

        public BulletinPacket(int packetID, int length)
            : base(packetID, length)
        {
        }

        public enum PacketSubType : byte
        {
            DisplayBoard = 0,
            SendPostSummary = 1,
            SendPostMessage = 2,
            RequestPostMessage = 3,
            RequestPostSummary = 4,
            RequestPostCreation = 5,
            RequestPostRemove = 6
        }

        public static void LogPacket(string Description, byte[] Data)
        {
#if BULLETIN_DEBUG 
            Console.WriteLine( "{0}: Size={1}", Description, Data.Length ); 
            foreach( byte b in Data ) 
                Console.Write( ( b < 0x10 ? "0" : "" ) + string.Format( "{0:X} ", b ) ); 
            Console.WriteLine( string.Empty ); 
#endif
        }
    }
    #endregion

    public sealed class BulletinBoardOpenPacket : BulletinPacket
    {
        public BulletinBoardOpenPacket(BulletinBoard Board)
            : base(0x71)
        {
            // Don't need much room here (just enough to take into account a 
            // non-default bulletin board name) 
            EnsureCapacity(256);

            // Get the name of the bulletin board 
            string BoardName = Board.Name == null ? "Bulletin Board" : Board.Name;

            // Fill the packet data 
            UnderlyingStream.Write((byte)BulletinPacket.PacketSubType.DisplayBoard);
            UnderlyingStream.Write((int)Board.Serial);
            UnderlyingStream.WriteAsciiNull(BoardName);

            // Log the raw packet info 
            BulletinPacket.LogPacket("BulletinBoardOpenPacket", UnderlyingStream.ToArray());
        }
    }

    public sealed class BulletinBoardFillItemsPacket6 : BulletinPacket
    {
        public BulletinBoardFillItemsPacket6(BulletinBoard Board)
            : base(0x3c)
        {
            // Since the maximum size of a packet is 65535 and the overhead of the 
            // segment is 19 characters, lets ensure the maximum capacity is available 
            EnsureCapacity(65535);

            // There will be a check to allow only a maximum of 3072 posts to be shown 
            // on a single bulletin board 
            // 3072x19 bytes for an item segment = 58368 bytes (should be very safe) 

            // Get all of the items associated with this bulletin board 
            //ArrayList OriginalPosts = Board.Items;
            //List<Item> OriginalPosts = Board.Items;
            List<Item> OriginalPosts = new List<Item>();
            foreach (Item item in World.Items.Values)
            {
                // Add each global post to the global post list 
                if (item is BulletinBoardPost)
                {
                    //all posts go on every messagebaord
                    //if (item.Parent == Board)
                    OriginalPosts.Add(item);
                }
            }

            // Do a deep copy of each post on the board (need to do this so that 
            // the replies can be added to the outgoing message without modifiying 
            // the original array list) 
            List<Item> AllPosts = new List<Item>();
            //ArrayList AllPosts = new ArrayList(OriginalPosts.Count);
            foreach (BulletinBoardPost OriginalPost in OriginalPosts)
                AllPosts.Add(OriginalPost);

            // Add any global posts to the top of the new list. 
            // Global posts can not be replied to since they don't belong to 
            // a specific bulletin board. 
            if (BulletinBoardGlobalPostList.m_GlobalPostList.Count > 0)
                AllPosts.InsertRange(0, BulletinBoardGlobalPostList.m_GlobalPostList);

            // Collect any replies to posts and insert them into the array list 
            // as long as the array list contains less than 3072 items 
            for (int x = 0; x < AllPosts.Count; x++)
            {
                // Get the current post object in the list 
                BulletinBoardPost Post = AllPosts[x] as BulletinBoardPost;

                // Check if the object retrieved from the list was a post 
                if (Post != null)
                {
                    // Check to see if this post item has any child posts 
                    if (Post.Items.Count > 0)
                    {
                        // Collect all child posts (replies) and insert them 
                        // into the original array list after the current posts 
                        // position. 
                        foreach (BulletinBoardPost Reply in Post.Items)
                            AllPosts.Add(Reply); // Add the reply posts to the end 
                    }
                }
            }

            // Fill in the mandatory pieces of the packet 
            UnderlyingStream.Write((short)AllPosts.Count);

            // Check each root item for any children 
            foreach (BulletinBoardPost Post in AllPosts)
            {
                UnderlyingStream.Write((int)Post.Serial);
                UnderlyingStream.Write((byte)0x0E); // Model High 
                UnderlyingStream.Write((byte)0xB0); // Model Low 
                UnderlyingStream.Write((byte)0x00); // Unknown 
                UnderlyingStream.Write((byte)0x00); // Items In Stack High 
                UnderlyingStream.Write((byte)0x00); // Items In Stack Low 
                UnderlyingStream.Write((byte)0x00); // X Position High 
                UnderlyingStream.Write((byte)0x3A); // X Position Low 
                UnderlyingStream.Write((byte)0x00); // Y Position High 
                UnderlyingStream.Write((byte)0x3A); // Y Position Low 
                UnderlyingStream.Write((int)Board.Serial);
                UnderlyingStream.Write((byte)0x00); // Colour High 
                UnderlyingStream.Write((byte)0x00); // Colour Low 
            }

            // Log the raw packet info 
            BulletinPacket.LogPacket("BulletinBoardFillItemsPacket6", UnderlyingStream.ToArray());
        }
    }

    public sealed class BulletinBoardFillItemsPacket : BulletinPacket
    {
        public BulletinBoardFillItemsPacket(BulletinBoard Board)
            : base(0x3c)
        {
            // Since the maximum size of a packet is 65535 and the overhead of the 
            // segment is 19 characters, lets ensure the maximum capacity is available 
            EnsureCapacity(65535);

            // There will be a check to allow only a maximum of 3072 posts to be shown 
            // on a single bulletin board 
            // 3072x19 bytes for an item segment = 58368 bytes (should be very safe) 

            // Get all of the items associated with this bulletin board 
            //ArrayList OriginalPosts = Board.Items;
            //List<Item> OriginalPosts = Board.Items;
            List<Item> OriginalPosts = new List<Item>();
            foreach (Item item in World.Items.Values)
            {
                // Add each global post to the global post list 
                if (item is BulletinBoardPost)
                {
                    //all posts go on every messagebaord
                    //if (item.Parent == Board)
                        OriginalPosts.Add(item);
                }
            }

            // Do a deep copy of each post on the board (need to do this so that 
            // the replies can be added to the outgoing message without modifiying 
            // the original array list) 
            List<Item> AllPosts = new List<Item>();
            //ArrayList AllPosts = new ArrayList(OriginalPosts.Count);
            foreach (BulletinBoardPost OriginalPost in OriginalPosts)
                AllPosts.Add(OriginalPost);

            // Add any global posts to the top of the new list. 
            // Global posts can not be replied to since they don't belong to 
            // a specific bulletin board. 
            if (BulletinBoardGlobalPostList.m_GlobalPostList.Count > 0)
                AllPosts.InsertRange(0, BulletinBoardGlobalPostList.m_GlobalPostList);

            // Collect any replies to posts and insert them into the array list 
            // as long as the array list contains less than 3072 items 
            for (int x = 0; x < AllPosts.Count; x++)
            {
                // Get the current post object in the list 
                BulletinBoardPost Post = AllPosts[x] as BulletinBoardPost;

                // Check if the object retrieved from the list was a post 
                if (Post != null)
                {
                    // Check to see if this post item has any child posts 
                    if (Post.Items.Count > 0)
                    {
                        // Collect all child posts (replies) and insert them 
                        // into the original array list after the current posts 
                        // position. 
                        foreach (BulletinBoardPost Reply in Post.Items)
                            AllPosts.Add(Reply); // Add the reply posts to the end 
                    }
                }
            }

            // Fill in the mandatory pieces of the packet 
            UnderlyingStream.Write((short)AllPosts.Count);

            // Check each root item for any children 
            foreach (BulletinBoardPost Post in AllPosts)
            {
                UnderlyingStream.Write((int)Post.Serial);
                UnderlyingStream.Write((byte)0x0E); // Model High 
                UnderlyingStream.Write((byte)0xB0); // Model Low 
                UnderlyingStream.Write((byte)0x00); // Unknown 
                UnderlyingStream.Write((byte)0x00); // Items In Stack High 
                UnderlyingStream.Write((byte)0x00); // Items In Stack Low 
                UnderlyingStream.Write((byte)0x00); // X Position High 
                UnderlyingStream.Write((byte)0x3A); // X Position Low 
                UnderlyingStream.Write((byte)0x00); // Y Position High 
                UnderlyingStream.Write((byte)0x3A); // Y Position Low 
                UnderlyingStream.Write((byte)0x00);
                UnderlyingStream.Write((int)Board.Serial);
                UnderlyingStream.Write((byte)0x00); // Colour High 
                UnderlyingStream.Write((byte)0x00); // Colour Low 
            }

            // Log the raw packet info 
            BulletinPacket.LogPacket("BulletinBoardFillItemsPacket", UnderlyingStream.ToArray());
        }
    }

    public sealed class BulletinBoardSendPostSummaryPacket : BulletinPacket
    {
        public BulletinBoardSendPostSummaryPacket(BulletinBoard Board, BulletinBoardPost Post)
            : base(0x71)
        {
            // Set the maximum size 
            EnsureCapacity(1024);

            // Fill the packet data 
            UnderlyingStream.Write((byte)BulletinPacket.PacketSubType.SendPostSummary);
            int BoardSerial = Board.Serial;
            if ((Post.RootParent != null) && (Post.RootParent is BulletinBoard))
                BoardSerial = (((Item)(Post.RootParent)).Serial);
            UnderlyingStream.Write((int)BoardSerial); // Bulletin Board Serial 
            UnderlyingStream.Write((int)Post.Serial); // Post Serial 
            int ParentSerial = Board.Serial;
            if ((Post.Parent != null) && ((Post.Parent is BulletinBoard) || (Post.Parent is BulletinBoardPost)))
                ParentSerial = (((Item)(Post.Parent)).Serial);
            UnderlyingStream.Write((int)(ParentSerial == BoardSerial ? 0 : ParentSerial)); // Parent Serial (if it is a reply) 
            UnderlyingStream.Write((byte)(Post.Author.Length + 1));
            UnderlyingStream.WriteAsciiNull(Post.Author);
            UnderlyingStream.Write((byte)(Post.Subject.Length + 1));
            UnderlyingStream.WriteAsciiNull(Post.Subject);
            UnderlyingStream.Write((byte)(Post.Date.Length + 1));
            UnderlyingStream.WriteAsciiNull(Post.Date);

            // Log the raw packet info 
            BulletinPacket.LogPacket("BulletinBoardSendPostSummaryPacket", UnderlyingStream.ToArray());
        }
    }

    public sealed class BulletinBoardPostPacket : BulletinPacket
    {
        public BulletinBoardPostPacket(BulletinBoardPost Post)
            : base(0x71)
        {
            // Set the maximum size 
            EnsureCapacity(65535);

            // Fill the packet data 
            UnderlyingStream.Write((byte)BulletinPacket.PacketSubType.SendPostMessage);
            int BoardSerial = 0;
            if ((Post.RootParent != null) && (Post.RootParent is BulletinBoard))
                BoardSerial = (((Item)(Post.RootParent)).Serial);
            UnderlyingStream.Write((int)BoardSerial); // Bulletin Board Serial 
            UnderlyingStream.Write((int)Post.Serial); // Post Serial 
            UnderlyingStream.Write((byte)(Post.Author.Length + 1));
            UnderlyingStream.WriteAsciiNull(Post.Author);
            UnderlyingStream.Write((byte)(Post.Subject.Length + 1));
            UnderlyingStream.WriteAsciiNull(Post.Subject);
            UnderlyingStream.Write((byte)(Post.Date.Length + 1));
            UnderlyingStream.WriteAsciiNull(Post.Date);

            #region Unknown Constant
            // Some constant that is needed (DON'T Change Anything in here unless 
            // you've figured out what the contents of the packet do) 
            //"\x01\x90\x83\xea\x06\x15\x2e\x07\x1d\x17\x0f\x07\x37\x1f\x7b 
            // \x05\xeb\x20\x3d\x04\x66\x20\x4d\x04\x66\x0e\x75\x00\x00" 
            UnderlyingStream.Write((byte)0x01);
            UnderlyingStream.Write((byte)0x90);
            UnderlyingStream.Write((byte)0x83);
            UnderlyingStream.Write((byte)0xEA);
            UnderlyingStream.Write((byte)0x06);
            UnderlyingStream.Write((byte)0x15);
            UnderlyingStream.Write((byte)0x2E);
            UnderlyingStream.Write((byte)0x07);
            UnderlyingStream.Write((byte)0x1D);
            UnderlyingStream.Write((byte)0x17);
            UnderlyingStream.Write((byte)0x0F);
            UnderlyingStream.Write((byte)0x07);
            UnderlyingStream.Write((byte)0x37);
            UnderlyingStream.Write((byte)0x1F);
            UnderlyingStream.Write((byte)0x7B);
            UnderlyingStream.Write((byte)0x05);
            UnderlyingStream.Write((byte)0xEB);
            UnderlyingStream.Write((byte)0x20);
            UnderlyingStream.Write((byte)0x3D);
            UnderlyingStream.Write((byte)0x04);
            UnderlyingStream.Write((byte)0x66);
            UnderlyingStream.Write((byte)0x20);
            UnderlyingStream.Write((byte)0x4D);
            UnderlyingStream.Write((byte)0x04);
            UnderlyingStream.Write((byte)0x66);
            UnderlyingStream.Write((byte)0x0E);
            UnderlyingStream.Write((byte)0x75);
            UnderlyingStream.Write((byte)0x00);
            UnderlyingStream.Write((byte)0x00);
            #endregion

            // Lets assume that all of the previous data that is of varying size 
            // was completely full (255 characters for Author, Subject, and Date) 
            // The total size of the packet so far would then be (in bytes): 
            // 1+2+1+4+4+1+255+1+255+1+255+29 = 809 Bytes 
            // That leaves 65535-809 = 64726 Bytes for the remaining lines of the 
            // message.  Each line can contain a maximum of 255 characters (including the terminating null) 
            // And there can only be a total of 255 lines so, to be safe we are only 
            // going to allow 250 lines max!  That will result in a total of 
            // 250x256 = 64000 Bytes total for a complete message (way under the 64766 limit) 

            // Set the maximum number of lines 
            int MaxLines = (Post.Message.Length < 250 ? Post.Message.Length : 250);
            UnderlyingStream.Write((byte)MaxLines);

            // Add each line (up to the maximum number of lines) 
            for (int x = 0; ((x < Post.Message.Length) && (x < 250)); x++)
            {
                UnderlyingStream.Write((byte)(Post.Message[x].Length + 1));
                UnderlyingStream.WriteAsciiNull(Post.Message[x]);
            }

            // Log the raw packet info 
            BulletinPacket.LogPacket("BulletinBoardPostPacket", UnderlyingStream.ToArray());
        }
    }

    public sealed class AddPostReplyItemPacket : BulletinPacket
    {
        public AddPostReplyItemPacket(BulletinBoardPost Post)
            : base(0x25, 21)
        {
            
            // Add the post to the bulletin board 
            UnderlyingStream.Write((int)Post.Serial);
            UnderlyingStream.Write((byte)0x0e); // Model High 
            UnderlyingStream.Write((byte)0xb0); // Model Low 
            UnderlyingStream.Write((byte)0x00); // Unknown 
            UnderlyingStream.Write((byte)0x00); // Item Count High 
            UnderlyingStream.Write((byte)0x00); // Item Count Low 
            UnderlyingStream.Write((byte)0x00); // X Location High 
            UnderlyingStream.Write((byte)0x00); // X Location Low 
            UnderlyingStream.Write((byte)0x00); // Y Location High 
            UnderlyingStream.Write((byte)0x00); // Y Location Low 
            UnderlyingStream.Write((byte)0x00);
            UnderlyingStream.Write((int)(((Item)(Post.RootParent)).Serial)); // Parent item serial number 
            UnderlyingStream.Write((byte)0x00); // Colour High 
            UnderlyingStream.Write((byte)0x00); // Colour Low 

            BulletinPacket.LogPacket("AddPostReplyItemPacket", UnderlyingStream.ToArray());
        }
    }
}