namespace ImoveisConnect.Application.Core.Result
{
    public class ResultHelper
    {
        /// <summary>
        /// Constroi um NotFoundResult com mensagem.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msgError"></param>
        /// <returns></returns>
        public static NotFoundResult<T> NotFound<T>(string error)
        {
            return new NotFoundResult<T>(error);
        }

        /// <summary>
        /// Constroi um InvalidResult com mensagem.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="error"></param>
        /// <returns></returns>
        public static InvalidResult<T> Invalid<T>(string error)
        {
            return new InvalidResult<T>(error);
        }

        /// <summary>
        /// Constroi um NotFoundResult com lista de mensagens.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msgError"></param>
        /// <returns></returns>
        public static NotFoundResult<T> NotFound<T>(List<string> errors)
        {
            return new NotFoundResult<T>(errors);
        }

        /// <summary>
        /// Constroi um InvalidResult com lista de mensagens.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="error"></param>
        /// <returns></returns>
        public static InvalidResult<T> Invalid<T>(List<string> errors)
        {
            return new InvalidResult<T>(errors);
        }

        /// <summary>
        /// Constroi um SuccessResult com dado padrão
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static SuccessResult<T> Success<T>(T data, string success = null)
        {
            return new SuccessResult<T>(data, success);
        }

        /// <summary>
        /// Constroi um PageResult com dado e total de registros
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public static PageResult<T> Page<T>(T data, int totalRecords, string success = null)
        {
            return new PageResult<T>(data, totalRecords, success);
        }
    }
}
