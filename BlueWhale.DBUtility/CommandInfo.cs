using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace BlueWhale.DBUtility
{
    public enum EffentNextType
    {
        /// <summary>
        /// No effect on other statements
        /// </summary>
        None,
        /// <summary>
        /// The current statement must be in the format "select count(1) from ...". If the result exists, continue execution; if not, rollback the transaction.
        /// </summary>
        WhenHaveContine,
        /// <summary>
        /// The current statement must be in the format "select count(1) from ...". If the result does not exist, continue execution; if it exists, rollback the transaction.
        /// </summary>
        WhenNoHaveContine,
        /// <summary>
        /// The number of rows affected by the current statement must be greater than 0; otherwise, rollback the transaction.
        /// </summary>
        ExcuteEffectRows,
        /// <summary>
        /// Trigger event - The current statement must be in the format "select count(1) from ...". If the result does not exist, continue execution; if it exists, rollback the transaction.
        /// </summary>
        SolicitationEvent

    }
    public class CommandInfo
    {
        public object ShareObject = null;
        public object OriginalData = null;
        event EventHandler _solicitationEvent;
        public event EventHandler SolicitationEvent
        {
            add
            {
                _solicitationEvent += value;
            }
            remove
            {
                _solicitationEvent -= value;
            }
        }
        public void OnSolicitationEvent()
        {
            if (_solicitationEvent != null)
            {
                _solicitationEvent(this, new EventArgs());
            }
        }
        public string CommandText;
        public System.Data.Common.DbParameter[] Parameters;
        public EffentNextType EffentNextType = EffentNextType.None;
        public CommandInfo()
        {

        }
        public CommandInfo(string sqlText, SqlParameter[] para)
        {
            this.CommandText = sqlText;
            this.Parameters = para;
        }
        public CommandInfo(string sqlText, SqlParameter[] para, EffentNextType type)
        {
            this.CommandText = sqlText;
            this.Parameters = para;
            this.EffentNextType = type;
        }
    }
}