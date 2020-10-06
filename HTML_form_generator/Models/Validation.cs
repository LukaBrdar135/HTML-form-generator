using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace HTML_form_generator.Models
{
    public class Validation
    {
        public static bool validate(NameValueCollection nvc)
        {
            int numberOfSelects = Convert.ToInt32(nvc["numberOfSelects"]);

            for (int i = 0; i < numberOfSelects; i++)
            {
                string select = nvc["select" + i];
                if (!string.IsNullOrEmpty(select))
                {
                    // if radio
                    if (select == "radio")
                    {
                        if (string.IsNullOrEmpty(nvc["radioGroupName" + i]))
                        {
                            return false;
                        }
                        string nmbrOfRadio = nvc["radioNumber" + i];
                        if (!string.IsNullOrEmpty(nmbrOfRadio))
                        {
                            int radioNumber = Convert.ToInt32(nmbrOfRadio);
                            for (int j = 0; j < radioNumber; j++)
                            {
                                if (string.IsNullOrEmpty(nvc["radio" + i + "input" + j]))
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            return false;
                        }

                    } //end if radio

                    // if checkbox
                    else if (select == "checkbox")
                    {
                        if (string.IsNullOrEmpty(nvc["cboxGroupName" + i]))
                        {
                            return false;
                        }
                        string nmbrOfCheck = nvc["cboxNumber"+ i];
                        if (!string.IsNullOrEmpty(nmbrOfCheck))
                        {
                            int cboxNumber = Convert.ToInt32(nmbrOfCheck);
                            for (int j = 0; j < cboxNumber; j++)
                            {
                                if (string.IsNullOrEmpty(nvc["cbox" + i + "input" + j]))
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            return false;
                        }

                    } //end if checkbox

                    // if range
                    else if (select == "range")
                    {
                        if (string.IsNullOrEmpty(nvc["rangeName" + i]) || string.IsNullOrEmpty(nvc["rangeMin" + i]) || string.IsNullOrEmpty(nvc["rangeMax" + i]))
                        {
                            return false;
                        }

                    } //end if range

                    else if (string.IsNullOrEmpty(nvc["input" + i]))
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }

                if (nvc["submitButton"] == "")
                {
                    return false;
                }
            }
            return true;
        }
    }
}