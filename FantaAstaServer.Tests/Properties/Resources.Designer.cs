﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Il codice è stato generato da uno strumento.
//     Versione runtime:4.0.30319.42000
//
//     Le modifiche apportate a questo file possono provocare un comportamento non corretto e andranno perse se
//     il codice viene rigenerato.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FantaAstaServer.Tests.Properties {
    using System;
    
    
    /// <summary>
    ///   Classe di risorse fortemente tipizzata per la ricerca di stringhe localizzate e così via.
    /// </summary>
    // Questa classe è stata generata automaticamente dalla classe StronglyTypedResourceBuilder.
    // tramite uno strumento quale ResGen o Visual Studio.
    // Per aggiungere o rimuovere un membro, modificare il file con estensione ResX ed eseguire nuovamente ResGen
    // con l'opzione /str oppure ricompilare il progetto VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Restituisce l'istanza di ResourceManager nella cache utilizzata da questa classe.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("FantaAstaServer.Tests.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Esegue l'override della proprietà CurrentUICulture del thread corrente per tutte le
        ///   ricerche di risorse eseguite utilizzando questa classe di risorse fortemente tipizzata.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Cerca una stringa localizzata simile a CREATE TYPE &quot;auction_mode&quot; AS ENUM (
        ///  &apos;turn_based&apos;,
        ///  &apos;real_time&apos;
        ///);
        ///
        ///CREATE TYPE &quot;player_role&quot; AS ENUM (
        ///  &apos;goalkeper&apos;,
        ///  &apos;defender&apos;,
        ///  &apos;midfielder&apos;,
        ///  &apos;striker&apos;
        ///);
        ///
        ///CREATE TYPE &quot;auction_status&quot; AS ENUM (
        ///  &apos;initialized&apos;,
        ///  &apos;in_lobby&apos;,
        ///  &apos;created&apos;,
        ///  &apos;started&apos;,
        ///  &apos;ended&apos;
        ///);
        ///
        ///CREATE TYPE &quot;player_call_order_mode&quot; AS ENUM (
        ///  &apos;by_role&apos;,
        ///  &apos;unordered&apos;
        ///);
        ///
        ///CREATE TYPE &quot;batch_status&quot; AS ENUM (
        ///  &apos;ongoing&apos;,
        ///  &apos;end&apos;
        ///);
        ///
        ///CREATE TABLE &quot;Users&quot; (
        ///  &quot;id&quot; INTEGER GENERATED BY DEFAULT AS I [stringa troncata]&quot;;.
        /// </summary>
        internal static string Db {
            get {
                return ResourceManager.GetString("Db", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Cerca una risorsa localizzata di tipo System.Byte[].
        /// </summary>
        internal static byte[] OnePlayer {
            get {
                object obj = ResourceManager.GetObject("OnePlayer", resourceCulture);
                return ((byte[])(obj));
            }
        }
    }
}
