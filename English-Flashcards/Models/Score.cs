using English_Flashcards.Models.Base;

namespace English_Flashcards.Models
{
    internal class Score : Model
    {

        #region ushort - Correct 
        /// <summary>
        /// 
        /// </summary>
        private ushort _Correct;

		/// <summary>
		/// 
		/// </summary>
		public ushort Correct
		{
			get { return _Correct; }
			set => Set(ref _Correct, value);
		}
        #endregion

		#region ushort - Wrong 
        /// <summary>
        /// 
        /// </summary>
        private ushort _Wrong;

		/// <summary>
		/// 
		/// </summary>
		public ushort Wrong
		{
			get { return _Wrong; }
			set => Set(ref _Wrong, value);
		}
		#endregion
	}
}
