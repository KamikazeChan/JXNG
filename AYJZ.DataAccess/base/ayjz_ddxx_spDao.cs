﻿
using System;
using System.Text;
using System.Reflection;
using System.Data;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using AYJZ.Entities;
using JITE.CIS.Framework.DBProviders;
using System.Data.Common;
namespace AYJZ.DataAccess
{
    public partial class ayjz_ddxx_spDao:IDataAccess
    {
        const bool isDebug = true;
        private static readonly string DalSql = "SELECT ID,DDZL,EIID,SHZH,LXR,LXDH,DZ,YPGZ,DJ,AYID,AYXM,NL,BZ,KSSJ,JSSJ,ZQ,DDZT,SFSP,SPID,SPJG,CZLX FROM ayjz_ddxx_sp where 1=1 ";
        
    
        private int RunCommandWithTransatcion(ayjz_ddxx_spInfo ent, string vSql, IDbTransaction TRANS)
        {
            if (null == TRANS)
            {
                MySqlParameter[] paras = new MySqlParameter[ent.Column.Count];
                for (int i = 0; i < ent.Column.Count; i++)
                {
                    paras[i] = new MySqlParameter();
                    paras[i].ParameterName = ent.Column[i].FieldName;
                    paras[i].DbType = ent.Column[i].FieldType;
                    paras[i].Value = ent.Column[i].FieldValue;
                }
                return  DataBaseManage.ExecuteSql(vSql, paras);
            }
            else
            {
                System.Data.IDbCommand CM = TRANS.Connection.CreateCommand();
                CM.CommandText = vSql;
                CM.CommandType = CommandType.Text;
                CM.Transaction = TRANS;
                GetEntityDeleteParameter(CM, ent);
                try
                {
                    return CM.ExecuteNonQuery();
                }
                catch (System.Exception e)
                {
                    if (isDebug)
                        throw new Exception(e.Message);
                    return 0;
                }
            }
        }

        public int Insert(BaseEntitie ent, IDbTransaction TRANS)
        {
            StringBuilder insSQL = new StringBuilder(" INSERT INTO ayjz_ddxx_sp (");
            bool isFirstValue = true;
            StringBuilder sp = new StringBuilder();
            ColumnCollection _column = ent.Column;
            for (int i = 0; i < _column.Count; i++)
            {
                if (isFirstValue)
                {
                    isFirstValue = false;
                    insSQL.Append(_column[i].FieldName);
                    sp.Append("@" + _column[i].FieldName);
                }
                else
                {
                    insSQL.Append("," + _column[i].FieldName);
                    sp.Append(",@" + _column[i].FieldName);
                }
            }
            insSQL.Append(") values (" + sp.ToString() + ")");
            return RunCommandWithTransatcion((ayjz_ddxx_spInfo)ent, insSQL.ToString(), TRANS);
        }

        public int Delete(BaseEntitie ent, IDbTransaction TRANS)
        {
            string s_DelSQL = " DELETE FROM ayjz_ddxx_sp   WHERE  SPID=@SPID ";
            return RunCommandWithTransatcion((ayjz_ddxx_spInfo)ent, s_DelSQL, TRANS);
        }

        public int Update(BaseEntitie ent, IDbTransaction TRANS)
        {
            StringBuilder s_UpdSQL = new StringBuilder(" UPDATE ayjz_ddxx_sp SET ");
            bool isFirstValue = true;
            ColumnCollection _column = ent.Column;//entity.TableFieldsName;
            for (int i = 0; i < _column.Count; i++)
            {
                if (isFirstValue)
                {
                    isFirstValue = false;
                    s_UpdSQL.Append(_column[i].FieldName);
                    s_UpdSQL.Append("=");
                    s_UpdSQL.Append("@" + _column[i].FieldName);
                }
                else
                {
                    s_UpdSQL.Append("," + _column[i].FieldName);
                    s_UpdSQL.Append("=");
                    s_UpdSQL.Append("@" + _column[i].FieldName);
                }
            }
            s_UpdSQL.Append("    WHERE  SPID=@SPID  ");
            return RunCommandWithTransatcion((ayjz_ddxx_spInfo)ent, s_UpdSQL.ToString(), TRANS);
        }

        /// <summary>
        /// 根据Id得到
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        public ayjz_ddxx_spInfo Getayjz_ddxx_sp( long SPID)
        {
              ayjz_ddxx_spInfo ent = null;
            string sql = DalSql;
            sql = sql + " And  SPID=@SPID ";
            MySqlParameter[] paras = new MySqlParameter[]
            {
                new MySqlParameter("SPID",SPID)
            };
            using(DbDataReader reader = DataBaseManage.ExecuteReader(sql, paras))
			{
				if (reader.Read())
				{
					ent = new ayjz_ddxx_spInfo();
                    SetEnt(ent, reader);
				}
				
       		}
            return ent;
        }

        /// <summary>
        /// 得到列表
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        public List<ayjz_ddxx_spInfo> Getayjz_ddxx_spList(string Where)
        {
            List<ayjz_ddxx_spInfo> list = new List<ayjz_ddxx_spInfo>();
            using(DbDataReader reader = DataBaseManage.ExecuteReader(DalSql+Where))
			{
                while (reader.Read())
                {
                   ayjz_ddxx_spInfo ent = new ayjz_ddxx_spInfo();
                    SetEnt(ent, reader);
                    list.Add(ent);
                }
            }
            return list;
        }

        public void SetEnt(ayjz_ddxx_spInfo ent, IDataReader dr)
        {
            ent.ID = MyConvert.ToLong(dr["ID"]);
            ent.DDZL = MyConvert.ToString(dr["DDZL"]);
            ent.EIID = MyConvert.ToLong(dr["EIID"]);
            ent.SHZH = MyConvert.ToString(dr["SHZH"]);
            ent.LXR = MyConvert.ToString(dr["LXR"]);
            ent.LXDH = MyConvert.ToString(dr["LXDH"]);
            ent.DZ = MyConvert.ToString(dr["DZ"]);
            ent.YPGZ = MyConvert.ToString(dr["YPGZ"]);
            ent.DJ = MyConvert.ToDecimal(dr["DJ"]);
            ent.AYID = MyConvert.ToLong(dr["AYID"]);
            ent.AYXM = MyConvert.ToString(dr["AYXM"]);
            ent.NL = MyConvert.ToLong(dr["NL"]);
            ent.BZ = MyConvert.ToString(dr["BZ"]);
            ent.KSSJ = MyConvert.ToString(dr["KSSJ"]);
            ent.JSSJ = MyConvert.ToString(dr["JSSJ"]);
            ent.ZQ = MyConvert.ToLong(dr["ZQ"]);
            ent.DDZT = MyConvert.ToString(dr["DDZT"]);
            ent.SFSP = MyConvert.ToString(dr["SFSP"]);
            ent.SPID = MyConvert.ToLong(dr["SPID"]);
            ent.SPJG = MyConvert.ToString(dr["SPJG"]);
            ent.CZLX = MyConvert.ToString(dr["CZLX"]);
        }

        private IDbDataParameter _CreateParameter(string szParameter, object sdtObject, ParameterDirection pdDirection, System.Data.IDbDataParameter sParameter)
        {
            sParameter.ParameterName = szParameter;
            sParameter.Value = sdtObject;
            sParameter.Direction = pdDirection;
            return sParameter;
        }

        protected virtual void GetEntityDeleteParameter(System.Data.IDbCommand CM, BaseEntitie ent)
        {
            ColumnCollection _column = ent.Column;
            for (int i = 0; i < _column.Count; i++)
            {
                System.Data.IDbDataParameter sParameter = CM.CreateParameter();
                sParameter.ParameterName = _column[i].FieldName;
                sParameter.Value = _column[i].FieldValue;
                sParameter.DbType = _column[i].FieldType;
                CM.Parameters.Add(sParameter);
            }
        }       
    }
}
