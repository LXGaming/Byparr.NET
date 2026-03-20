using System.Net;
using LXGaming.Byparr.Models;
using LXGaming.Byparr.Models.Responses;

namespace LXGaming.Byparr.Utilities;

public static class Extensions {

    public static bool IsSuccessStatus(this HealthResponse response) {
        return string.Equals(response.Message, HealthResponse.WorkingMessage);
    }

    public static V1Response EnsureSuccessStatus(this V1Response response) {
        if (!response.IsSuccessStatus()) {
            throw new HttpRequestException(response.Message, null, HttpStatusCode.InternalServerError);
        }

        return response;
    }

    public static bool IsSuccessStatus(this V1Response response) {
        return response.Status == Status.Ok;
    }

    public static Solution EnsureSuccessStatus(this Solution? solution) {
        ArgumentNullException.ThrowIfNull(solution);
        if (!solution.IsSuccessStatus()) {
            throw new HttpRequestException($"Solution status does not indicate success: {solution.Status}.", null,
                (HttpStatusCode) solution.Status);
        }

        return solution;
    }

    public static bool IsSuccessStatus(this Solution? solution) {
        ArgumentNullException.ThrowIfNull(solution);
        return solution.Status is >= 200 and <= 299;
    }
}