
using System; 
using Server; 
using System.Collections;
using System.Collections.Generic;
using Server.Items; 
using Server.ContextMenus; 
using Server.Misc; 
using Server.Mobiles; 
using Server.Network; 

namespace Server.Mobiles 
{ 
    public class BaseHire : BaseCreature 
    {
        private int m_Pay = 1;
 
        private bool m_IsHired; 
        private int m_HoldGold = 8; 
        private Timer m_PayTimer;

        private DateTime m_LastPay;
        
        public BaseHire( AIType   AI ): base( AI, FightMode.Aggressor, 10, 1, 0.4, 2 ) 
        { 
        } 

        public BaseHire(): base( AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.4, 2 ) 
        { 
        } 

        public BaseHire( Serial serial ) : base( serial ) 
        { 
        } 

        public override void Serialize( GenericWriter writer ) 
        { 
            base.Serialize( writer ); 

            writer.Write( (int) 1 ); // version 

            writer.Write((DateTime)m_LastPay);
            writer.Write( (bool)m_IsHired ); 
            writer.Write( (int)m_HoldGold ); 
        } 

        public override void Deserialize( GenericReader reader ) 
        { 
            base.Deserialize( reader ); 

            int version = reader.ReadInt();

            if (version >= 1)
                m_LastPay = reader.ReadDateTime();

            if (this.ActiveSpeed != 0.4)
                this.ActiveSpeed = 0.4;

            m_IsHired = reader.ReadBool(); 
            m_HoldGold = reader.ReadInt(); 
          
            m_PayTimer = new PayTimer( this ); 
            m_PayTimer.Start(); 

        } 

        private static Hashtable m_HireTable = new Hashtable(); 

        public static Hashtable HireTable 
        { 
            get{ return m_HireTable; } 
        } 

        public override bool KeepsItemsOnDeath{ get{ return true; } } 
        private int m_GoldOnDeath = 0; 
        public override bool OnBeforeDeath() 
        { 
            // Stop the pay timer if its running 
            if( m_PayTimer != null ) 
                m_PayTimer.Stop(); 

            m_PayTimer = null; 

            // Get all of the gold on the hireling and add up the total amount 
            if( this.Backpack != null ) 
            { 
                Item[] AllGold = this.Backpack.FindItemsByType( typeof(Gold), true ); 
                if( AllGold != null ) 
                { 
                    foreach( Gold g in AllGold ) 
                        this.m_GoldOnDeath += g.Amount; 
                } 
            } 

            return base.OnBeforeDeath(); 

        } 

        public override void OnDeath( Container c ) 
        { 

            if( this.m_GoldOnDeath > 0 ) 
                c.DropItem( new Gold( this.m_GoldOnDeath ) ); 
            base.OnDeath( c ); 
        } 

        [CommandProperty( AccessLevel.Administrator )] 
        public bool IsHired 
        { 
            get 
            { 
                return m_IsHired; 
            } 
            set 
            { 
                if ( m_IsHired== value ) 
                    return; 

                m_IsHired= value; 
                Delta( MobileDelta.Noto ); 
                InvalidateProperties(); 
            } 
        } 

        #region [ GetOwner ] 
        public virtual Mobile GetOwner() 
        { 
            if( !Controlled ) 
                return null; 
            Mobile Owner = ControlMaster; 
          
            m_IsHired = true; 
          
            if( Owner == null ) 
                return null; 
          
            if( Owner.Deleted || Owner.Map != this.Map || !Owner.InRange( Location, 30 ) ) 
            { 
                Say( true, "Hmmm.  I seem to have lost my master." ); // Hmmm.  I seem to have lost my master. 
                BaseHire.HireTable.Remove( Owner ); 
                SetControlMaster( null ); 
                return null; 
            } 

            return Owner; 
        } 
        #endregion 

        #region [ AddHire ] 
        public virtual bool AddHire( Mobile m ) 
        { 
            Mobile owner = GetOwner(); 

            //if( owner != null ) 
            //{ 
            //    m.SendLocalizedMessage( 1043283, owner.Name ); // I am following ~1_NAME~. 
            //    return false; 
            //} 

            if( SetControlMaster( m ) ) 
            { 
                m_IsHired = true; 
                return true; 
            } 
          
            return false; 
        } 
        #endregion 

        #region [ Payday ] 
        public virtual bool Payday( BaseHire m ) 
        {
            if (m is HireBard)
                m_Pay = 20;
            else if (m is HireBeggar)
                m_Pay = 10;
            else if (m is HireFighter || m is HireMercenary || m is HireWarrior)
                m_Pay = 60;
            else if (m is HireMage)
                m_Pay = 60;
            else if (m is HirePaladin)
                m_Pay = 80;
            else if (m is HirePeasant)
                m_Pay = 10;
            else if (m is HireRanger)
                m_Pay = 70;
            else if (m is HireSailor)
                m_Pay = 50;
            else if (m is HireThief)
                m_Pay = 60;
            else if (m is HirePirate)
                m_Pay = 70;

            return true; 
        } 
        #endregion 

        #region [ OnDragDrop ] 
        public override bool OnDragDrop( Mobile from, Item item ) 
        {
            Payday(this);
            if( m_Pay != 0 ) 
            { 
                // Is the creature already hired 
                if( ControlMaster == from || ControlMaster == null ) 
                { 
                    // Is the item the payment in gold 
                    if( item is Gold ) 
                    { 
                        // Is the payment in gold sufficient 
                        if( item.Amount >= m_Pay ) 
                        { 
                            // Check if this mobile already has a hire 
                            /*BaseHire hire = (BaseHire)m_HireTable[from]; 

                            if( hire != null && !hire.Deleted && hire.GetOwner() == from ) 
                            { 
                                SayTo( from, true, "I see you already have an escort."); // I see you already have an escort. 
                                return false; 
                            } */

                            // Try to add the hireling as a follower 
                            if( AddHire(from) == true ) 
                            {
                                m_HoldGold += item.Amount;
                                m_HireTable[from] = this; 

                                SayTo(from, true, String.Format("I thank thee for paying me. I will work for thee for {0} days.", (int)m_HoldGold / m_Pay));//"I thank thee for paying me. I will work for thee for ~1_NUMBER~ days.", (int)item.Amount / m_Pay ); 
                                m_LastPay = DateTime.Now;
                                if (m_PayTimer == null)
                                {
                                    m_PayTimer = new PayTimer(this);
                                    m_PayTimer.Start();
                                }

                                return true; 
                            } 
                            else 
                                return false; 
                        } 
                        else 
                        {
                            SayTo(from, true, "Thou must pay me more than this!");
                        } 
                    }
                } 
                else 
                { 
                    Say( true, "I have already been hired." );// I have already been hired. 
                } 
            } 
            else 
            { 
                SayTo( from, true, "I have no need for that. " ); // I have no need for that. 
            }

            return false;

            //return base.OnDragDrop( from, item ); 
        } 
        #endregion 

        #region [ OnSpeech ] 
        internal void SayHireCost() 
        {
            Say(true, String.Format("I am available for hire for {0} gold coins a day. If thou dost give me gold, I will work for thee.", m_Pay)); // "I am available for hire for ~1_AMOUNT~ gold coins a day. If thou dost give me gold, I will work for thee." 
        } 

        public override void OnSpeech( SpeechEventArgs e ) 
        {    
            if( !e.Handled && e.Mobile.InRange( this, 6 ) ) 
            { 
                int[] keywords = e.Keywords; 
                string speech = e.Speech; 

                // Check for a greeting or 'Hire' 
                if( ( e.HasKeyword( 0x003B ) == true ) || ( e.HasKeyword( 0x0162 ) == true ) || Insensitive.Speech(e.Speech, "mercenary") || Insensitive.Speech(e.Speech, "servant") || Insensitive.Speech(e.Speech, "work")) 
                { 
                    e.Handled = Payday( this ); 
                    this.SayHireCost(); 
                }
                else if (Insensitive.Speech(e.Speech, "report") && e.Mobile == GetOwner())
                {
                    SayTo(e.Mobile, true, String.Format("I currently accept orders from {0}.",GetOwner().Name));
                    string message = "";
                    TimeSpan TimeRemaining = (m_LastPay + TimeSpan.FromMinutes(30.0 * Math.Floor((double)(m_HoldGold / m_Pay)))) - DateTime.Now;
                    if ( TimeRemaining >= TimeSpan.FromMinutes(120.0))
                        message = "I am wonderfully happy with my job.";
                    else if ( TimeRemaining >= TimeSpan.FromMinutes(60.0))
                        message = "I am extremely happy with my job.";
                    else if (TimeRemaining >= TimeSpan.FromMinutes(40.0))
                        message = "I am very happy with my job.";
                    else if ( TimeRemaining > TimeSpan.FromMinutes(30.0))
                        message = "I am rather happy with my job.";
                    else if ( TimeRemaining >= TimeSpan.FromMinutes(25.0))
                        message = "I am happy with my job.";
                    else if ( TimeRemaining >= TimeSpan.FromMinutes(20.0))
                        message = "I am content with my job.";
                    else if ( TimeRemaining >= TimeSpan.FromMinutes(15.0))
                        message = "I am content with my job, I suppose.";
                    else if ( TimeRemaining >= TimeSpan.FromMinutes(10.0))
                        message = "I am unhappy with my job.";
                    else if ( TimeRemaining >= TimeSpan.FromMinutes(5.0))
                        message = "I am rather unhappy with my job.";
                    else
                        message = "I am extremely unhappy with my job.";

                    SayTo(e.Mobile, true, message);
                }
            } 

            base.OnSpeech( e ); 
        } 
        #endregion    
        
        #region [ GetContextMenuEntries ] 
        public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry>  list ) 
        { 
            Mobile Owner = GetOwner(); 
          
            if( Owner == null ) 
            { 
                base.GetContextMenuEntries( from, list ); 
                list.Add( new HireEntry( from, this ) ); 
            } 
            else 
                base.GetContextMenuEntries( from, list ); 
        } 
        #endregion 
        
        #region [ Class PayTimer ] 
        private class PayTimer : Timer 
        { 
            private BaseHire m_Hire; 
          
            public PayTimer( BaseHire vend ) : base( TimeSpan.FromMinutes( 30.0 ), TimeSpan.FromMinutes( 30.0 ) ) 
            { 
                m_Hire = vend; 
                Priority = TimerPriority.OneMinute; 
            } 
          
            protected override void OnTick() 
            { 
                int m_Pay = m_Hire.m_Pay; 
                if( m_Hire.m_HoldGold <= m_Pay ) 
                { 
                    // Get the current owner, if any (updates HireTable) 
                    Mobile owner = m_Hire.GetOwner(); 

                    m_Hire.Say( true, "I thank thee for thy kindness!" );
                    m_Hire.Delete(); 
                } 
                else 
                { 
                    m_Hire.m_HoldGold -= m_Pay; 
                } 
            } 
        } 
        #endregion 

        #region [ Class HireEntry ] 
        public class HireEntry : ContextMenuEntry 
        { 
            private Mobile m_Mobile; 
            private BaseHire m_Hire; 

            public HireEntry( Mobile from, BaseHire hire ) : base( 6120, 3 )    
            { 
                m_Hire = hire; 
                m_Mobile = from; 
            } 
          
            public override void OnClick()    
            {    
                m_Hire.Payday(m_Hire); 
                m_Hire.SayHireCost(); 
            } 
        } 
        #endregion 
    }    
} 
