using PR.OpenGym.Data;
using PR.OpenGym.Web.DTOS.FaceTerminalOperations.FaceTerminalModels;
using PR.OpenGym.Web.DTOS.FaceTerminalOperations.FaceTerminalModels.AccessControl.AcsEvent;
using PR.OpenGym.Web.DTOS.FaceTerminalOperations.FaceTerminalModels.AccessControl.CardInfo;
using PR.OpenGym.Web.DTOS.FaceTerminalOperations.FaceTerminalModels.AccessControl.UserInfo;
using PR.OpenGym.Web.DTOS.FaceTerminalOperations.FaceTerminalModels.FDILib;
using PR.OpenGym.Web.Models;

namespace PR.OpenGym.Web.Services
{
    public interface IFaceTerminalService
    {
        Task<string> TakeDeviceFace();
        Task<int> GetUserCount();
        Task<RecordRequest> RegisterUserFlow(AssociateViewModel associateViewModel, int numberOfDays, byte[] image);
        Task<bool> TestConnection();

        /// <summary>
        /// Metodo para crear un almacen de imagenes
        /// </summary>
        /// <param name="fDLibRequest"></param>
        /// <returns></returns>
        Task<FDLibResponse> CreateFDLib(FDLibRequest fDLibRequest);


        /// <summary>
        /// Metodo para crear la Informacion del usuario
        /// </summary>
        /// <param name="userInfoRequest"></param>
        /// <returns></returns>
        Task<RecordResponse> CreateUserInfoRecord(RecordRequest userInfoRequest);

        /// <summary>
        /// Metodo para Actualizar la Informacion del usuario
        /// </summary>
        /// <param name="userInfoRequest"></param>
        /// <returns></returns>
        Task<RecordResponse> ModifyUserInfoRecord(RecordRequest userInfoRequest);

        /// <summary>
        /// Metodo para asignar imagen 'Cara' a un usuario
        /// </summary>
        /// <returns></returns>
        Task<FaceDataRecordResponse> CreateFaceDataRecord(FaceDataRecordRequest faceDataRecordRequest , byte[] image);

        /// <summary>
        /// Crear Una tarjeta de identidad a un usuario
        /// </summary>
        /// <param name="cIRecordRequest"></param>
        /// <returns></returns>
        Task<GenericResponse> CreateCardRecord(CIRecordRequest cIRecordRequest);


        /// <summary>
        /// Buscar usuarios registrados en la terminal
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <returns></returns>
        Task<SearchResponse> SearchUsers(SearchRequest searchRequest);

        /// <summary>
        /// Obtner imagen del dispositivo del usuario por Id
        /// </summary>
        /// <param name="faceTerminalID"></param>
        /// <returns></returns>
        Task<string> GetFaceTerminalImageFlow(string faceTerminalID);

        /// <summary>
        /// Crear una visita y generar QR para acceso por weigand en la terminal
        /// </summary>
        /// <returns></returns>
        Task<string> CreateVisitFlow();

        /// <summary>
        /// Get Logs de la terminal 'simulando las visitas'
        /// </summary>
        /// <param name="acsEventRequest"></param>
        /// <returns></returns>
        Task<AcsEventResponse> GetEventLogs(AcsEventRequest acsEventRequest);

        /// <summary>
        /// Flow utilizado para camiar las elnombre usuario y  fechas valida de acceso del socio el Inicio y fin del acceso
        /// </summary>
        /// <param name="FPID">Id de la persona</param>
        /// <param name="fullname">Nombre persona</param>
        /// <param name="from">Fecahe deesde cuando</param>
        /// <param name="numberOfDays">Representacion de dias a agregar a from  7,15,30,,365 etc..</param>
        /// <returns></returns>
        Task<bool> ModifyNameStartTimeEndTimeFlow(string FPID, string fullname, DateTime from, int numberOfDays);

        /// <summary>
        /// Utilizado para eliminar de la terminal
        /// </summary>
        /// <param name="FPID">Numero de empleado ID</param>
        /// <returns></returns>
        Task<GenericResponse> DeletePerson(string FPID);
    }
}
