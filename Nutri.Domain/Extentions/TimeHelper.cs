namespace Nutri.Domain.Extentions;

public static class TimeHelper
{
    public static bool IsThisYear(this DateTime time)
    {
        if (time.Year == DateTime.Now.Year)
            return true;

        else 
            return false;
    }

    public static bool IsThisMonth(this DateTime time)
    {
        if (!time.IsThisYear())
            return false;

        if (time.Month == DateTime.Now.Month)
            return true;

        else
            return false;
    }

    public static bool IsThisWeek(this DateTime time)
    {
        if (!(time.IsThisYear()))
            return false;
    }

    public static bool IsToday(this DateTime time)
    {
        if (!time.IsThisYear())
            return false;

        if (!time.IsThisMonth())
            return false;

        if (time.Day == DateTime.Now.Day)
            return true;

        else
            return false;
    }
}