using System;
using System.Collections.Generic;

/// <summary>
/// Runtime string table for the interface.
///
/// The settings screen already exposed a language selector (<see cref="RealBusLanguage"/>
/// plus <c>SettingsManager.CycleLanguage</c>), but nothing ever read the value, so
/// switching language changed nothing on screen. This is the lookup that makes the
/// setting real: <see cref="LocalizedText"/> binds a label to a key, and
/// <see cref="SettingsManager"/> calls <see cref="SetLanguage"/> whenever the setting
/// is applied, so every bound label re-reads its string in the new language.
///
/// Translations are author-drafted first passes rather than a professional
/// localisation pass - see the note in bug.md. The table is deliberately built as
/// one row per key with one entry per language so <see cref="ValidateTable"/> can
/// prove no language is missing a string.
/// </summary>
public static class Localization
{
    /// <summary>Languages the interface can render, in <see cref="RealBusLanguage"/> order.</summary>
    public static readonly RealBusLanguage[] SupportedLanguages =
    {
        RealBusLanguage.English,
        RealBusLanguage.Swahili,
        RealBusLanguage.French,
        RealBusLanguage.German,
        RealBusLanguage.Spanish
    };

    /// <summary>Raised after <see cref="SetLanguage"/> actually changes the language.</summary>
    public static event Action LanguageChanged;

    public static RealBusLanguage Current { get; private set; } = RealBusLanguage.English;

    static bool applied;

    // key -> one string per language, indexed by (int)RealBusLanguage.
    static readonly Dictionary<string, string[]> Table = new Dictionary<string, string[]>(StringComparer.Ordinal)
    {
        // ── Settings tabs ───────────────────────────────────────────────
        { "tab.controls",      L("CONTROLS",      "VIDHIBITI",        "COMMANDES",               "STEUERUNG",          "CONTROLES") },
        { "tab.audio",         L("AUDIO",         "SAUTI",            "AUDIO",                   "AUDIO",              "AUDIO") },
        { "tab.display",       L("DISPLAY",       "ONYESHO",          "AFFICHAGE",               "ANZEIGE",            "PANTALLA") },
        { "tab.accessibility", L("ACCESSIBILITY", "UFIKIVU",          "ACCESSIBILITÉ",           "BARRIEREFREIHEIT",   "ACCESIBILIDAD") },

        // ── Controls ────────────────────────────────────────────────────
        { "set.steeringScheme",      L("Steering Scheme",  "Mpango wa Usukani",            "Mode de direction",             "Lenkungsart",              "Modo de dirección") },
        { "set.steeringScheme.desc", L("Tilt, virtual steering wheel, buttons or keys", "Kuinamisha, usukani wa kawaida, vitufe au vibonye", "Inclinaison, volant virtuel, boutons ou clavier", "Neigung, virtuelles Lenkrad, Tasten oder Tastatur", "Inclinación, volante virtual, botones o teclas") },
        { "set.drivingAssists",      L("Driving Assists",  "Visaidizi vya Uendeshaji",     "Aides à la conduite",           "Fahrassistenten",          "Asistencias de conducción") },
        { "set.drivingAssists.desc", L("Traction control, ABS and lane-keeping assist", "Kidhibiti mtelezo, ABS na msaada wa kubaki kwenye njia", "Contrôle de traction, ABS et aide au maintien de voie", "Traktionskontrolle, ABS und Spurhalteassistent", "Control de tracción, ABS y asistencia de carril") },
        { "set.leftHanded",          L("Left-Handed Layout", "Mpangilio wa Kushoto",       "Disposition gaucher",           "Linkshänder-Layout",       "Diseño para zurdos") },
        { "set.leftHanded.desc",     L("Mirrors steering and pedals for left hand dominance", "Hubadilisha usukani na makanyagio kwa mkono wa kushoto", "Inverse la direction et les pédales pour les gauchers", "Spiegelt Lenkung und Pedale für Linkshänder", "Invierte dirección y pedales para zurdos") },
        { "set.touchSize",           L("Touch Control Size", "Ukubwa wa Vidhibiti vya Mguso", "Taille des commandes tactiles", "Größe der Touch-Bedienung", "Tamaño de controles táctiles") },
        { "set.touchSize.desc",      L("Scale cockpit touch buttons on display", "Kupanga ukubwa wa vitufe vya mguso kwenye skrini", "Ajuste la taille des boutons tactiles du cockpit", "Skaliert die Touch-Tasten im Cockpit", "Escala los botones táctiles de la cabina") },
        { "set.steeringResponse",    L("Steering Response", "Mwitikio wa Usukani",          "Réactivité de la direction",    "Lenkempfindlichkeit",      "Respuesta de dirección") },
        { "set.steeringResponse.desc", L("Sensitivity ramp on wheel turns", "Kiwango cha mwitikio wa kugeuza usukani", "Progression de la sensibilité du volant", "Empfindlichkeitskurve beim Lenken", "Progresión de sensibilidad al girar") },
        { "set.steeringDeadZone",    L("Steering Dead Zone", "Eneo Lisilo na Athari",       "Zone morte de direction",       "Lenkungstotzone",          "Zona muerta de dirección") },
        { "set.steeringDeadZone.desc", L("Center play to prevent inadvertent drift", "Nafasi ya kati kuzuia kuteleza bila kusudi", "Jeu au centre pour éviter les dérives", "Spiel in der Mitte gegen ungewolltes Ziehen", "Juego central para evitar desvíos") },
        { "set.customLayout",        L("Custom Control Layout", "Mpangilio Maalum wa Vidhibiti", "Disposition des commandes",  "Eigenes Bedienlayout",     "Diseño de controles") },
        { "set.customLayout.desc",   L("Reposition on-screen buttons to fit your grip", "Weka vitufe vya skrini mahali panapokufaa", "Repositionnez les boutons selon votre prise", "Bedienelemente frei anordnen", "Reubica los botones a tu medida") },
        { "set.customLayout.action", L("EDIT LAYOUT", "HARIRI MPANGO", "MODIFIER", "ANPASSEN", "EDITAR") },

        // ── Audio ───────────────────────────────────────────────────────
        { "set.masterVolume",      L("Master Volume", "Sauti Kuu", "Volume général", "Gesamtlautstärke", "Volumen general") },
        { "set.masterVolume.desc", L("Overall game audio balance", "Usawazishaji wa sauti ya mchezo", "Équilibre audio général", "Gesamte Audiobalance", "Balance de audio general") },
        { "set.sfx",               L("Bus SFX & Engine", "Sauti za Basi na Injini", "Sons du bus et moteur", "Bus-SFX & Motor", "Sonidos del bus y motor") },
        { "set.sfx.desc",          L("Diesel motor, air brakes, doors and horn", "Injini ya dizeli, breki za hewa, milango na honi", "Moteur diesel, freins à air, portes et klaxon", "Dieselmotor, Druckluftbremsen, Türen und Hupe", "Motor diésel, frenos de aire, puertas y claxon") },
        { "set.music",             L("Cabin Radio & Music", "Redio ya Basi na Muziki", "Radio et musique de cabine", "Kabin Radio & Musik", "Radio y música de cabina") },
        { "set.music.desc",        L("Transit background music and announcements", "Muziki wa usuli na matangazo ya safari", "Musique d'ambiance et annonces", "Hintergrundmusik und Ansagen", "Música de fondo y avisos") },
        { "set.subtitles",         L("Subtitles", "Manukuu", "Sous-titres", "Untertitel", "Subtítulos") },
        { "set.subtitles.desc",    L("Display on-screen dialogue and alerts", "Onyesha mazungumzo na tahadhari kwenye skrini", "Affiche les dialogues et alertes", "Zeigt Dialoge und Warnungen an", "Muestra diálogos y alertas") },
        { "set.haptics",           L("Haptic Feedback", "Mtetemo", "Retour haptique", "Haptisches Feedback", "Respuesta háptica") },
        { "set.haptics.desc",      L("Vibration on road kerbs and collisions", "Mtetemo kwenye kona za barabara na migongano", "Vibration sur les bordures et collisions", "Vibration bei Bordsteinen und Kollisionen", "Vibración en bordillos y colisiones") },

        // ── Display ─────────────────────────────────────────────────────
        { "set.quality",          L("Visual Quality", "Ubora wa Picha", "Qualité visuelle", "Grafikqualität", "Calidad visual") },
        { "set.quality.desc",     L("Graphics fidelity, shadows and reflection probes", "Ubora wa michoro, vivuli na mwanga", "Fidélité graphique, ombres et reflets", "Grafiktreue, Schatten und Reflexionen", "Fidelidad gráfica, sombras y reflejos") },
        { "set.framerate",        L("Target Frame Rate", "Kiwango cha Fremu", "Fréquence d'images cible", "Ziel-Bildrate", "Fotogramas objetivo") },
        { "set.framerate.desc",   L("Refresh rate clamp for smooth driving", "Kikomo cha fremu kwa uendeshaji laini", "Limite de rafraîchissement pour une conduite fluide", "Bildratenbegrenzung für flüssiges Fahren", "Límite de refresco para una conducción fluida") },
        { "set.adaptive",         L("Adaptive Scaling", "Upanguaji Unaojibadilisha", "Mise à l'échelle adaptative", "Adaptive Skalierung", "Escalado adaptable") },
        { "set.adaptive.desc",    L("Dynamically adjusts resolution to maintain target FPS", "Hubadilisha ubora ili kudumisha fremu lengwa", "Ajuste la résolution pour tenir la cadence cible", "Passt die Auflösung an die Ziel-Bildrate an", "Ajusta la resolución para mantener los FPS") },
        { "set.reducedMotion",    L("Reduced Motion", "Mwenendo Pungufu", "Mouvement réduit", "Bewegung reduzieren", "Movimiento reducido") },
        { "set.reducedMotion.desc", L("Tones down camera roll sway and camera shake", "Hupunguza mteremko na mtikisiko wa kamera", "Réduit le roulis et les secousses de caméra", "Reduziert Kameraschwenk und Verwackeln", "Reduce el balanceo y la vibración de cámara") },

        // ── Accessibility ───────────────────────────────────────────────
        { "set.language",       L("Language", "Lugha", "Langue", "Sprache", "Idioma") },
        { "set.language.desc",  L("Local transit translation", "Tafsiri ya usafiri wa eneo", "Traduction du transport local", "Übersetzung des Nahverkehrs", "Traducción del transporte local") },
        { "set.largeText",      L("Large Text", "Maandishi Makubwa", "Grand texte", "Große Schrift", "Texto grande") },
        { "set.largeText.desc", L("Increases UI text size for enhanced legibility", "Huongeza ukubwa wa maandishi kwa usomaji rahisi", "Augmente la taille du texte de l'interface", "Vergrößert die UI-Schrift für bessere Lesbarkeit", "Aumenta el tamaño del texto de la interfaz") },
        { "set.highContrast",   L("High Contrast", "Tofauti Kubwa ya Rangi", "Contraste élevé", "Hoher Kontrast", "Alto contraste") },
        { "set.highContrast.desc", L("Amplifies contrast on dashboard gauges and HUD", "Huongeza tofauti ya rangi kwenye dashibodi na HUD", "Renforce le contraste du tableau de bord et de l'ATH", "Verstärkt den Kontrast von Armaturen und HUD", "Refuerza el contraste del tablero y el HUD") },
        { "set.colourBlind",    L("Colour-Blind Icons", "Alama za Wasioona Rangi", "Icônes daltoniennes", "Farbenblinde Symbole", "Iconos daltónicos") },
        { "set.colourBlind.desc", L("Adds glyphs to colored route status indicators", "Huongeza alama kwenye viashiria vya hali ya njia", "Ajoute des glyphes aux indicateurs d'itinéraire", "Ergänzt Symbole an farbigen Statusanzeigen", "Añade símbolos a los indicadores de ruta") },

        // ── Depot actions ───────────────────────────────────────────────
        { "set.depot",          L("Depot Downloads", "Upakuaji wa Depo", "Téléchargements du dépôt", "Depot-Downloads", "Descargas del depósito") },
        { "set.depot.desc",     L("Download offline maps, routes, and vehicle packs", "Pakua ramani, njia na mifuko ya magari bila mtandao", "Téléchargez cartes, itinéraires et packs de véhicules", "Offline-Karten, Routen und Fahrzeugpakete laden", "Descarga mapas, rutas y paquetes de vehículos") },
        { "set.depot.action",   L("OPEN DEPOT", "FUNGUA DEPO", "OUVRIR", "ÖFFNEN", "ABRIR") },
        { "set.credits",        L("Attribution & Credits", "Sifa na Shukrani", "Attribution et crédits", "Quellen & Credits", "Atribución y créditos") },
        { "set.credits.desc",   L("OpenStreetMap, Mapbox, and Overture data credits", "Shukrani za data za OpenStreetMap, Mapbox na Overture", "Crédits des données OpenStreetMap, Mapbox et Overture", "Datenquellen OpenStreetMap, Mapbox und Overture", "Créditos de datos de OpenStreetMap, Mapbox y Overture") },
        { "set.credits.action", L("CREDITS", "SHUKRANI", "CRÉDITS", "CREDITS", "CRÉDITOS") },

        // ── Modal headers ───────────────────────────────────────────────
        { "hdr.settings.title",   L("[LINE 101] BUS OPERATOR SETTINGS", "MLANGO 101: MIPANGILIO YA OPERETA", "[LIGNE 101] PARAMÈTRES OPÉRATEUR", "[LINIE 101] BETRIEBSEINSTELLUNGEN", "[LÍNEA 101] AJUSTES DEL OPERADOR") },
        { "hdr.settings.subtitle", L("TRANSIT TELEMETRY  •  VEHICLE CONFIGURATION", "TAKWIMU ZA SAFARI  •  MIPANGILIO YA GARI", "TÉLÉMÉTRIE  •  CONFIGURATION DU VÉHICULE", "TELEMETRIE  •  FAHRZEUGKONFIGURATION", "TELEMETRÍA  •  CONFIGURACIÓN DEL VEHÍCULO") },
        { "badge.depotOnline",    L("DEPOT ONLINE", "DEPO IPO MTANDAONI", "DÉPÔT EN LIGNE", "DEPOT ONLINE", "DEPÓSITO EN LÍNEA") },
        { "hdr.mobile.title",     L("[CABIN] MOBILE & PERFORMANCE", "KABINI: SIMU NA UTENDAJI", "[CABINE] MOBILE ET PERFORMANCE", "[KABINE] MOBIL & LEISTUNG", "[CABINA] MÓVIL Y RENDIMIENTO") },
        { "hdr.mobile.subtitle",  L("FRAME RATE  •  SCALING  •  STEERING RESPONSE", "FREMU  •  UKUBWA  •  MWITIKIO WA USUKANI", "IMAGES  •  ÉCHELLE  •  DIRECTION", "BILDRATE  •  SKALIERUNG  •  LENKUNG", "FPS  •  ESCALADO  •  DIRECCIÓN") },
        { "badge.optimized",      L("OPTIMIZED", "IMEBORESHWA", "OPTIMISÉ", "OPTIMIERT", "OPTIMIZADO") },
        { "hdr.pause.title",      L("[TRANSIT SERVICE PAUSED]", "HUDUMA YA SAFARI IMESIMAMA", "[SERVICE EN PAUSE]", "[LINIENDIENST PAUSIERT]", "[SERVICIO EN PAUSA]") },
        { "hdr.pause.subtitle",   L("DEPOT DISPATCH IDLE  •  TRIP SUSPENDED", "KITUO HAKIFANYI KAZI  •  SAFARI IMESIMAMISHWA", "DISPATCH INACTIF  •  TRAJET SUSPENDU", "LEITSTELLE INAKTIV  •  FAHRT UNTERBROCHEN", "CENTRAL INACTIVA  •  VIAJE SUSPENDIDO") },
        { "badge.standby",        L("STANDBY", "TAAYARI", "VEILLE", "BEREITSCHAFT", "EN ESPERA") },
        { "hdr.downloads.title",    L("[DEPOT] DOWNLOADABLE CITY CONTENT", "[DEPO] MAUDHUI YA MJI YANAYOPAKULIWA", "[DÉPÔT] CONTENU DE VILLE TÉLÉCHARGEABLE", "[DEPOT] LADBARE STADTINHALTE", "[DEPÓSITO] CONTENIDO DE CIUDAD DESCARGABLE") },
        { "hdr.downloads.subtitle", L("Keep base install small. Stream only requested city routes.", "Weka usakinishaji mdogo. Pakua njia za miji unayotaka tu.", "Installation légère : ne téléchargez que les lignes demandées.", "Basisinstallation klein halten. Nur gewünschte Stadtlinien laden.", "Mantén la instalación ligera. Descarga solo las rutas pedidas.") },
        { "hdr.downloads.badge",    L("OFFLINE CACHE", "HAZINA YA NDANI", "CACHE HORS LIGNE", "OFFLINE-CACHE", "CACHÉ SIN CONEXIÓN") },
        { "hdr.fleet.title",        L("[FLEET] DOWNLOADABLE VEHICLE PACKS", "[FLEET] MIFUKO YA MAGARI YANAYOPAKULIWA", "[FLOTTE] PACKS DE VÉHICULES TÉLÉCHARGEABLES", "[FLOTTE] LADBARE FAHRZEUGPAKETE", "[FLOTA] PAQUETES DE VEHÍCULOS DESCARGABLES") },
        { "hdr.fleet.subtitle",     L("Install optional buses without bloating base package size.", "Sakinisha mabasi ya hiari bila kukuza kifurushi cha msingi.", "Installez des bus optionnels sans alourdir le paquet de base.", "Optionale Busse installieren, ohne das Basispaket zu vergrößern.", "Instala buses opcionales sin aumentar el paquete base.") },
        { "hdr.fleet.badge",        L("BUNDLE STORE", "DUKA LA VIFURUSHI", "BOUTIQUE DE PACKS", "PAKET-SHOP", "TIENDA DE PAQUETES") },
        { "hdr.credits.title",      L("REALBUS DATA & ATTRIBUTION", "DATA NA SHUKRANI ZA REALBUS", "DONNÉES ET ATTRIBUTIONS REALBUS", "REALBUS DATEN & QUELLEN", "DATOS Y ATRIBUCIÓN DE REALBUS") },
        { "hdr.credits.subtitle",   L("Open GIS standards and community data sources", "Viwango huria vya GIS na vyanzo vya data vya jamii", "Standards GIS ouverts et sources de données communautaires", "Offene GIS-Standards und Community-Datenquellen", "Estándares GIS abiertos y fuentes de datos comunitarias") },
        { "hdr.credits.badge",      L("OPEN DATA", "DATA HURIA", "DONNÉES OUVERTES", "OPEN DATA", "DATOS ABIERTOS") },
        { "hdr.daily.title",        L("[DISPATCH] DAILY ROUTE CHALLENGE", "[KITUO] CHANGAMOTO YA NJIA YA LEO", "[DISPATCH] DÉFI QUOTIDIEN D'ITINÉRAIRE", "[LEITSTELLE] TÄGLICHE ROUTEN-HERAUSFORDERUNG", "[CENTRAL] DESAFÍO DIARIO DE RUTA") },
        { "hdr.daily.subtitle",     L("Compete on today's featured transit line", "Shindana kwenye njia maalum ya leo", "Affrontez la ligne du jour", "Tritt auf der heutigen Strecke an", "Compite en la línea destacada de hoy") },
        { "hdr.daily.badge",        L("CHALLENGE", "CHANGAMOTO", "DÉFI", "HERAUSFORDERUNG", "DESAFÍO") },

        // ── Buttons ─────────────────────────────────────────────────────
        { "btn.settings",         L("⚙ SETTINGS", "⚙ MIPANGILIO", "⚙ PARAMÈTRES", "⚙ EINSTELLUNGEN", "⚙ AJUSTES") },
        { "btn.stopClose",        L("⯀ STOP / CLOSE", "⯀ SIMAMA / FUNGA", "⯀ ARRÊT / FERMER", "⯀ STOPP / SCHLIESSEN", "⯀ PARAR / CERRAR") },
        { "btn.stopBack",         L("⯀ STOP / BACK", "⯀ SIMAMA / RUDI", "⯀ ARRÊT / RETOUR", "⯀ STOPP / ZURÜCK", "⯀ PARAR / VOLVER") },
        { "btn.depotDownloads",   L("DEPOT DOWNLOADS", "UPAKUAJI WA DEPO", "TÉLÉCHARGEMENTS", "DEPOT-DOWNLOADS", "DESCARGAS") },
        { "btn.mobileOptimize",   L("MOBILE OPTIMIZE", "BOORESHA SIMU", "OPTIMISATION MOBILE", "MOBILE OPTIMIEREN", "OPTIMIZAR MÓVIL") },
        { "btn.resumeService",    L("⯈ RESUME SERVICE", "⯈ ENDELEA HUDUMA", "⯈ REPRENDRE LE SERVICE", "⯈ DIENST FORTSETZEN", "⯈ REANUDAR SERVICIO") },
        { "btn.operatorSettings", L("⚙ OPERATOR SETTINGS", "⚙ MIPANGILIO YA OPERETA", "⚙ PARAMÈTRES OPÉRATEUR", "⚙ BETRIEBSEINSTELLUNGEN", "⚙ AJUSTES DEL OPERADOR") },
        { "btn.editControls",     L("🎮 EDIT CONTROLS", "🎮 HARIRI VIDHIBITI", "🎮 MODIFIER COMMANDES", "🎮 STEUERUNG ANPASSEN", "🎮 EDITAR CONTROLES") },
        { "btn.depotMainMenu",    L("🚏 DEPOT / MAIN MENU", "🚏 DEPO / MENYU KUU", "🚏 DÉPÔT / MENU PRINCIPAL", "🚏 DEPOT / HAUPTMENÜ", "🚏 DEPÓSITO / MENÚ") },
        { "btn.offlineMap",       L("OFFLINE MAP", "RAMANI YA NDANI", "CARTE HORS LIGNE", "OFFLINE-KARTE", "MAPA SIN CONEXIÓN") },
        { "btn.worldData",        L("WORLD DATA", "DATA YA DUNIA", "DONNÉES DU MONDE", "WELTDATEN", "DATOS DEL MUNDO") },
        { "btn.allRoutes",        L("ALL ROUTES", "NJIA ZOTE", "TOUTES LES LIGNES", "ALLE ROUTEN", "TODAS LAS RUTAS") },
        { "btn.downloadRoute",    L("DOWNLOAD ROUTE", "PAKUA NJIA", "TÉLÉCHARGER LA LIGNE", "ROUTE LADEN", "DESCARGAR RUTA") },
        { "btn.cancel",           L("CANCEL", "GHAIRI", "ANNULER", "ABBRECHEN", "CANCELAR") },
        { "btn.removeCity",       L("REMOVE CITY", "ONDOA MJI", "SUPPRIMER LA VILLE", "STADT ENTFERNEN", "ELIMINAR CIUDAD") },
        { "btn.vehiclePacks",     L("VEHICLE PACKS", "MIFUKO YA MAGARI", "PACKS DE VÉHICULES", "FAHRZEUGPAKETE", "PAQUETES DE VEHÍCULOS") },
        { "btn.downloadInstall",  L("DOWNLOAD & INSTALL PACK", "PAKUA NA SAKINISHA KIFURUSHI", "TÉLÉCHARGER ET INSTALLER", "LADEN & INSTALLIEREN", "DESCARGAR E INSTALAR") },
        { "btn.removePacks",      L("REMOVE PACKS", "ONDOA MIFUKO", "SUPPRIMER LES PACKS", "PAKETE ENTFERNEN", "ELIMINAR PAQUETES") },
        { "btn.favourite",        L("★ FAVOURITE", "★ KIPENZI", "★ FAVORI", "★ FAVORIT", "★ FAVORITO") },
        { "btn.completeCity",     L("COMPLETE CITY", "MJII MZIMA", "VILLE COMPLÈTE", "KOMPLETTE STADT", "CIUDAD COMPLETA") },
        { "btn.driveChallenge",   L("⯈ DRIVE CHALLENGE", "⯈ ENDESHA CHANGAMOTO", "⯈ RELEVER LE DÉFI", "⯈ HERAUSFORDERUNG FAHREN", "⯈ AFRONTAR DESAFÍO") },
        { "btn.overtureDocs",     L("OVERTURE DATA DOCS", "NYARAKA ZA DATA ZA OVERTURE", "DOCS DONNÉES OVERTURE", "OVERTURE DATEN-DOKS", "DOCS DE DATOS OVERTURE") },

        // ── Status copy ────────────────────────────────────────────────
        { "status.ready",     L("Ready", "Tayari", "Prêt", "Bereit", "Listo") },
        { "status.noCities",  L("No cities available", "Hakuna miji inapatikana", "Aucune ville disponible", "Keine Städte verfügbar", "No hay ciudades disponibles") },
        { "status.loadingDaily", L("Loading today's route…", "Inapakia njia ya leo…", "Chargement de la ligne du jour…", "Heutige Strecke wird geladen…", "Cargando la ruta de hoy…") },

        // ── Common values ───────────────────────────────────────────────
        { "value.on",  L("On", "Washa", "Activé", "Ein", "Sí") },
        { "value.off", L("Off", "Zima", "Désactivé", "Aus", "No") },

        // ── Fuel & charging stations ────────────────────────────────────
        { "station.title",       L("FUEL & CHARGE", "KUJAZA NA CHAJI", "CARBURANT ET CHARGE", "TANKEN & LADEN", "COMBUSTIBLE Y CARGA") },
        { "station.fuel",        L("DIESEL", "DIZELI", "DIESEL", "DIESEL", "DIÉSEL") },
        { "station.charge",      L("ELECTRIC", "UMEME", "ÉLECTRIQUE", "ELEKTRISCH", "ELÉCTRICO") },
        { "station.level",       L("LEVEL", "KIWANGO", "NIVEAU", "FÜLLSTAND", "NIVEL") },
        { "station.target",      L("TARGET", "LENGO", "CIBLE", "ZIEL", "OBJETIVO") },
        { "station.full",        L("FULL", "KAMILI", "PLEIN", "VOLL", "LLENO") },
        { "station.add",         L("ADD", "ONGEZA", "AJOUTER", "HINZUFÜGEN", "AÑADIR") },
        { "station.estimated",   L("ESTIMATED", "MAKADIRIO", "ESTIMATION", "GESCHÄTZT", "ESTIMADO") },
        { "station.startFuel",   L("START REFUELLING", "ANZA KUJAZA", "COMMENCER LE PLEIN", "TANKEN STARTEN", "INICIAR REPOSTAJE") },
        { "station.startCharge", L("START CHARGING", "ANZA KUCHAJI", "COMMENCER LA CHARGE", "LADEN STARTEN", "INICIAR CARGA") },
        { "station.stop",        L("STOP", "SIMAMA", "ARRÊTER", "STOPP", "DETENER") },
        { "station.close",       L("CLOSE", "FUNGA", "FERMER", "SCHLIESSEN", "CERRAR") },
        { "station.tooFast",     L("STOP TO SERVICE", "SIMAMA ILI HUDUMA", "ARRÊTEZ-VOUS POUR LE SERVICE", "ZUM TANKEN ANHALTEN", "DETÉNGASE PARA EL SERVICIO") },
        { "station.fullTank",    L("TANK FULL", "TANKI KAMILI", "RÉSERVOIR PLEIN", "TANK VOLL", "DEPÓSITO LLENO") },
        { "station.noFunds",     L("INSUFFICIENT BALANCE", "SALIO HAKITOSHI", "SOLDE INSUFFISANT", "GUTHABEN ZU NIEDRIG", "SALDO INSUFICIENTE") },
        { "station.complete",    L("SERVICE COMPLETE", "HUDUMA KAMILI", "SERVICE TERMINÉ", "SERVICE ABGESCHLOSSEN", "SERVICIO COMPLETO") },
        { "station.stopped",     L("SERVICE STOPPED", "HUDUMA IMESIMAMA", "SERVICE ARRÊTÉ", "SERVICE GESTOPPT", "SERVICIO DETENIDO") },
        { "station.ready",       L("READY", "TAYARI", "PRÊT", "BEREIT", "LISTO") },
        { "station.active",      L("SESSION ALREADY RUNNING", "KIPINDI KINAENDELEA", "SESSION DÉJÀ EN COURS", "SITZUNG LÄUFT BEREITS", "SESIÓN YA EN CURSO") },
        { "station.busy",        L("ANOTHER VEHICLE IS BEING SERVICED", "GARI LINGINE LINAHUDUMIWA", "UN AUTRE VÉHICULE EST SERVI", "EIN ANDERES FAHRZEUG WIRD BEDIENT", "OTRO VEHÍCULO ESTÁ SIENDO ATENDIDO") },
        { "station.unsupported", L("NO COMPATIBLE SERVICE", "HAKUNA HUDUMA INAYOFAA", "AUCUN SERVICE COMPATIBLE", "KEIN PASSENDER SERVICE", "SIN SERVICIO COMPATIBLE") },
        { "station.progress",    L("SERVICE IN PROGRESS", "HUDUMA INAENDELEA", "SERVICE EN COURS", "SERVICE LÄUFT", "SERVICIO EN CURSO") },

        // ── HUD ─────────────────────────────────────────────────────────
        { "hud.freeDrive",   L("FREE DRIVE", "SAFARI HURU", "CONDUITE LIBRE", "FREIE FAHRT", "CONDUCCIÓN LIBRE") },
        { "hud.noSchedule",  L("◇ NO SCHEDULE", "◇ HAKUNA RATIBA", "◇ AUCUN HORAIRE", "◇ KEIN FAHRPLAN", "◇ SIN HORARIO") },
        { "hud.explore",     L("Explore safely", "Gundua kwa usalama", "Roulez prudemment", "Sicher erkunden", "Conduce con seguridad") },
        { "hud.routeNav",    L("ROUTE NAVIGATION", "UELEKEZAJI WA NJIA", "NAVIGATION D'ITINÉRAIRE", "ROUTENNAVIGATION", "NAVEGACIÓN DE RUTA") }
    };

    static string[] L(string english, string swahili, string french, string german, string spanish)
    {
        var row = new string[SupportedLanguages.Length];
        row[(int)RealBusLanguage.English] = english;
        row[(int)RealBusLanguage.Swahili] = swahili;
        row[(int)RealBusLanguage.French]  = french;
        row[(int)RealBusLanguage.German]  = german;
        row[(int)RealBusLanguage.Spanish] = spanish;
        return row;
    }

    /// <summary>Number of keys in the table.</summary>
    public static int KeyCount => Table.Count;

    /// <summary>Current language code for diagnostics.</summary>
    public static string LanguageCode(RealBusLanguage language)
    {
        switch (language)
        {
            case RealBusLanguage.Swahili: return "sw";
            case RealBusLanguage.French:  return "fr";
            case RealBusLanguage.German:  return "de";
            case RealBusLanguage.Spanish: return "es";
            default:                      return "en";
        }
    }

    /// <summary>String for <paramref name="key"/> in the active language.</summary>
    public static string Get(string key) => Get(Current, key);

    /// <summary>
    /// String for <paramref name="key"/> in a specific language. Falls back to the
    /// English entry, and finally to the key itself so a missing translation is
    /// visible in game instead of rendering blank.
    /// </summary>
    public static string Get(RealBusLanguage language, string key)
    {
        if (string.IsNullOrEmpty(key)) return string.Empty;
        if (!Table.TryGetValue(key, out var row)) return key;

        int index = (int)language;
        if (index >= 0 && index < row.Length && !string.IsNullOrEmpty(row[index]))
            return row[index];

        return !string.IsNullOrEmpty(row[(int)RealBusLanguage.English]) ? row[(int)RealBusLanguage.English] : key;
    }

    /// <summary>True when the table has a usable entry for the key.</summary>
    public static bool HasKey(string key) => !string.IsNullOrEmpty(key) && Table.ContainsKey(key);

    /// <summary>
    /// Every key present in the table. Used by the completeness test.
    /// </summary>
    public static IEnumerable<string> Keys => Table.Keys;

    /// <summary>
    /// Switches the active language and notifies bound labels. Returns true when the
    /// language actually changed, so repeated applies do not rebuild the UI.
    /// </summary>
    public static bool SetLanguage(RealBusLanguage language)
    {
        if (applied && language == Current) return false;

        Current = language;
        applied = true;
        LanguageChanged?.Invoke();
        return true;
    }

    /// <summary>
    /// Reports the keys that are missing a translation for any supported language.
    /// Returns an empty list for a complete table.
    /// </summary>
    public static List<string> ValidateTable()
    {
        var missing = new List<string>();
        foreach (var pair in Table)
        {
            string[] row = pair.Value;
            if (row == null || row.Length != SupportedLanguages.Length)
            {
                missing.Add(pair.Key);
                continue;
            }
            for (int i = 0; i < row.Length; i++)
                if (string.IsNullOrWhiteSpace(row[i]))
                {
                    missing.Add(pair.Key + ":" + LanguageCode((RealBusLanguage)i));
                    break;
                }
        }
        return missing;
    }
}
