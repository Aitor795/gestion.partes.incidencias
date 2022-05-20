using gestion.partes.incidencias.Modelo;
using gestion.partes.incidencias.Servicio;
using gestion.partes.incidencias.VO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace gestion.partes.incidencias.MVVM
{
    class MVRol : MVBase
    {
        private tfgEntities _tfgEntities;
        private RolServicio rolServicio;
        private PermisosRolServicio permisosRolServicio;
        private PermisoServicio permisoServicio;
        private List<permiso> listaPermisos;
        private List<rol> listaRoles;
        private ListCollectionView _listaPermisosTabla;

        public MVRol(tfgEntities ent)
        {
            _tfgEntities = ent;
            rolServicio = new RolServicio(ent);
            permisosRolServicio = new PermisosRolServicio(ent);
            permisoServicio = new PermisoServicio(ent);
            listaPermisos = permisoServicio.getAll().ToList();
            listaRoles = rolServicio.getAll().ToList();
            _listaPermisosTabla = new ListCollectionView(montarMatrizPermisosRoles());
        }

        private List<GestionPermisoVO> montarMatrizPermisosRoles()
        {
            List<GestionPermisoVO> listaGestionPermisosVO = new List<GestionPermisoVO>();

            for (int i = 0; i < listaPermisos.Count; i++)
            {
                GestionPermisoVO vo = new GestionPermisoVO();
                vo.permiso = listaPermisos[i];

                for (int j = 0; j < listaRoles.Count; j++)
                {
                    List<permisos_rol> permisosRol = listaRoles[j].permisos_rol.ToList();

                    for (int z = 0; z < permisosRol.Count; z++)
                    {
                        if (permisosRol[z].codigo_permiso == vo.permiso.codigo)
                        {
                            if (permisosRol[z].codigo_rol == "PROFESOR")
                            {
                                vo.profesor = true;
                            }
                            else if (permisosRol[z].codigo_rol == "TUTOR")
                            {
                                vo.tutor = true;
                            } 
                            else if (permisosRol[z].codigo_rol == "DIRECTIVO")
                            {
                                vo.directivo = true;
                            }
                            else if (permisosRol[z].codigo_rol == "ADMIN")
                            {
                                vo.administrador = true;
                            }
                        }
                    }
                }

                listaGestionPermisosVO.Add(vo);
            }

            return listaGestionPermisosVO;
        }

        public ListCollectionView listaPermisosTabla
        {
            get
            {
                return _listaPermisosTabla;
            }
        }

        public bool guardarPermisoRol (permisos_rol permisosRol)
        {
            permisos_rol permisosRolExistente = permisosRolServicio.buscarPermisosRol(permisosRol.codigo_rol, permisosRol.codigo_permiso);

            bool correcto = true;

            if (permisosRolExistente == null)
            {
                permisosRolServicio.add(permisosRol);

                try
                {
                    permisosRolServicio.save();
                }
                catch (DbUpdateException dbex)
                {
                    correcto = false;
                }
            }

            return correcto;
        }

        public void eliminaPermisoRol(string codigoRol, string codigoPermiso)
        {
            permisos_rol permisosRol = permisosRolServicio.buscarPermisosRol(codigoRol, codigoPermiso);

            if(permisosRol != null)
            {
                permisosRolServicio.delete(permisosRol);

                try
                {
                    permisosRolServicio.save();
                }
                catch (DbUpdateException dbex)
                {
                    System.Console.WriteLine(dbex.StackTrace);
                }
            }
        }
    }
}
