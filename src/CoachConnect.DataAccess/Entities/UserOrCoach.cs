namespace CoachConnect.DataAccess.Entities;

public abstract class UserOrCoach
{
    // This class is empty and serves as a common parent for User and Coach
    // It allows methods like AuthenticateUser to return both User and Coach. Also for Jwt>Userroles to be able to combine Id's.
}
