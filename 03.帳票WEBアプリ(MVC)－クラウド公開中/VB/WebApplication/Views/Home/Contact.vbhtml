@Code
    ViewData("Title") = "Contact"
End Code

<main aria-labelledby="title">
    <h2 id="title">@ViewBag.Title.</h2>
    <h3>@ViewBag.Message</h3>

    <address>
        3-29-2-401, Yatsu, Narashino-shi,
        Chiba 275-0026, Japan<br />
        <abbr title="Phone">Phone:</abbr> +81-47-452-0057
    </address>

    <address>
        <strong>Support:</strong>   <a href="mailto:info@pao.ac">info@pao.ac</a><br />
        <strong>Marketing:</strong> <a href="mailto:info@pao.ac">info@pao.ac</a>
    </address>
</main>
