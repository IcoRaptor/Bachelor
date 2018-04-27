using Framework.Debugging;
using System.Collections.Generic;

namespace AI.GOAP
{
    public static class GOAPResolver
    {
        #region Variables

        private static Dictionary<string, STATE_SYMBOL> _attribToSymbol =
            new Dictionary<string, STATE_SYMBOL>()
            {
            };

        #endregion

        public static STATE_SYMBOL GetSymbolFromAttribute(string attrib)
        {
            try
            {
                return _attribToSymbol[attrib];
            }
            catch
            {
                Debugger.LogFormat(LOG_TYPE.WARNING,
                    "AttribID '{0}' doesn't exist!\n",
                    attrib);
                return STATE_SYMBOL.ERROR;
            }
        }
    }
}