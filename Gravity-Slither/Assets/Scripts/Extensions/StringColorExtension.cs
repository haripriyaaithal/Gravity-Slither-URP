/// <summary>
/// This class contains extension functions which is used to Debug.Logs()
/// </summary>
public static class StringColorExtension {
    public static string ToAqua(this string str) {
        return $"<color=#00ffffff>{str}</color>";
    }

    public static string ToBlack(this string str) {
        return $"<color=#000000ff>{str}</color>";
    }

    public static string ToBlue(this string str) {
        return $"<color=#0000ffff>{str}</color>";
    }

    public static string ToBrown(this string str) {
        return $"<color=#a52a2aff>{str}</color>";
    }

    public static string ToCyan(this string str) {
        return $"<color=#00ffffff>{str}</color>";
    }

    public static string ToDarkBlue(this string str) {
        return $"<color=#0000a0ff>{str}</color>";
    }

    public static string ToFuchsia(this string str) {
        return $"<color=#ff00ffff>{str}</color>";
    }

    public static string ToGreen(this string str) {
        return $"<color=#008000ff>{str}</color>";
    }

    public static string ToLightGreen(this string str) {
        return $"<color=#bfff00ff>{str}</color>";
    }

    public static string ToLightGreen2(this string str) {
        return $"<color=#ccff00ff>{str}</color>";
    }

    public static string ToGrey(this string str) {
        return $"<color=#808080ff>{str}</color>";
    }

    public static string ToLightBlue(this string str) {
        return $"<color=#add8e6ff>{str}</color>";
    }

    public static string ToLime(this string str) {
        return $"<color=#00ff00ff>{str}</color>";
    }

    public static string ToMagenta(this string str) {
        return $"<color=#ff00ffff>{str}</color>";
    }

    public static string ToMaroon(this string str) {
        return $"<color=#800000ff>{str}</color>";
    }

    public static string ToNavy(this string str) {
        return $"<color=#000080ff>{str}</color>";
    }

    public static string ToOlive(this string str) {
        return $"<color=#808000ff>{str}</color>";
    }

    public static string ToOrange(this string str) {
        return $"<color=#ffa500ff>{str}</color>";
    }

    public static string ToPurple(this string str) {
        return $"<color=#800080ff>{str}</color>";
    }

    public static string ToAsh(this string str) {
        return $"<color=#A1A1A1>{str}</color>";
    }

    public static string ToRed(this string str) {
        return $"<color=#ff0000ff>{str}</color>";
    }

    public static string ToSilver(this string str) {
        return $"<color=#c0c0c0ff>{str}</color>";
    }

    public static string ToTeal(this string str) {
        return $"<color=#008080ff>{str}</color>";
    }

    public static string ToWhite(this string str) {
        return $"<color=#ffffffff>{str}</color>";
    }

    public static string ToYellow(this string str) {
        return $"<color=#ffff00ff>{str}</color>";
    }

    public static string ToBold(this string str) {
        return $"<b>{str}</b>";
    }
    
    public static string ToItalic(this string str) {
        return $"<i>{str}</i>";
    }
    
    public static string ToSize(this string str, int size) {
        return $"<size={size}>{str}</size>";
    }
}