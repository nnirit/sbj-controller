using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBJController
{
    /// <summary>
    /// This class represents the samples used in the experiment
    /// </summary>
    public class Sample
    {
        #region Private Members
        private int m_id;
        private Metal m_firstLayerMetal;
        private int m_firstLayerThickness;
        private Metal m_secondLayerMetal;
        private int m_secondLayerThickness;
        private int m_electrodeWidth;
        private bool m_isDry;
        private string m_solvent;
        private string m_molecule;
        private bool m_isRefabricated;
        private bool m_isAfterPirnha;
        private string m_commentss;
        #endregion

        #region Properties
        /// <summary>
        /// The Id of the sample glass
        /// </summary>
        public int ID
        {
            get { return m_id; }
            set { m_id = value; }
        }

        /// <summary>
        /// The first layer deposited metal
        /// </summary>
        public Metal FirstLayerMetal
        {
            get { return m_firstLayerMetal; }
            set { m_firstLayerMetal = value; }
        }

        /// <summary>
        /// First layer metal's thickness in nm
        /// </summary>
        public int FirstLayerThickness
        {
            get { return m_firstLayerThickness; }
            set { m_firstLayerThickness = value; }
        }

        /// <summary>
        /// The second layer deposited metal
        /// </summary>
        public Metal SecondLayerMetal
        {
            get { return m_secondLayerMetal; }
            set { m_secondLayerMetal = value; }
        }

        /// <summary>
        /// Second layer metal's thickness in nm
        /// </summary>
        public int SecondLayerThickness
        {
            get { return m_secondLayerThickness; }
            set { m_secondLayerThickness = value; }
        }

        /// <summary>
        /// Electrode's width in um
        /// </summary>
        public int ElectrodeWidth
        {
            get { return m_electrodeWidth; }
            set { m_electrodeWidth = value; }
        }

        /// <summary>
        /// Is the measurement carried on dry
        /// </summary>
        public bool Dry
        {
            get { return m_isDry; }
            set { m_isDry = value; }
        }

        /// <summary>
        /// The solvent in which the target molecule was dissolved in
        /// </summary>
        public string Solvent
        {
            get { return m_solvent; }
            set { m_solvent = value; }
        }

        /// <summary>
        /// The target molecule in the experiment
        /// </summary>
        public string Molecule
        {
            get { return m_molecule; }
            set { m_molecule = value; }
        }
         
        /// <summary>
        /// Indicates whether this is a refabricated sample
        /// </summary>
        public bool Refabricated
        {
            get { return m_isRefabricated; }
            set { m_isRefabricated = value; }
        }

        /// <summary>
        /// Indicates whether Pirnha was used with this sample perior to use
        /// </summary>
        public bool Piranha
        {
            get { return m_isAfterPirnha; }
            set { m_isAfterPirnha = value; }
        }

        /// <summary>
        /// Comments
        /// </summary>
        public string Comments
        {
            get { return m_commentss; }
            set { m_commentss = value; }
        }
#endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public Sample()
        {
            m_id = 0;
            m_firstLayerMetal = Metal.Cr;
            m_firstLayerThickness = 2;
            m_secondLayerMetal = Metal.Au;
            m_secondLayerThickness = 40;
            m_electrodeWidth = 30;
            m_isDry = false;
            m_solvent = string.Empty;
            m_molecule = string.Empty;
            m_isRefabricated = false; ;
            m_isAfterPirnha = false; ;
            m_commentss = string.Empty;
        }

        public Sample(int sampleId, Metal firstLayer, Metal secondLayer, int electrodeWidth,
                      bool isDry, string solvent, string molecule, bool isRefabricated, 
                      bool isAfetrPirnha, string comments)
        {
            m_id = sampleId;
            m_firstLayerMetal = firstLayer;
            m_secondLayerMetal = secondLayer;
            m_electrodeWidth = electrodeWidth;
            m_isDry = isDry;
            m_solvent = solvent;
            m_molecule = molecule;
            m_isRefabricated = isRefabricated;
            m_isAfterPirnha = isAfetrPirnha;
            m_commentss = comments;
        }
        #endregion

        #region Public Methods
        public override string ToString()
        {
            return ID.ToString();
        }
        #endregion
    }

    /// <summary>
    /// Metal enumerator
    /// </summary>
    public enum Metal
    {
        Cr,
        Ti,
        Au,
        Pt,
        Ag
    }
}
