import React from "react";
import ReactDOM from "react-dom";
import * as Sentry from "@sentry/react";
import { BrowserTracing } from "@sentry/tracing";

function init() {
  Sentry.init({
    dsn: "https://bbb1f9628a464d10a239a98362934082@o589726.ingest.sentry.io/6739177",
    integrations: [new BrowserTracing()],

    // Set tracesSampleRate to 1.0 to capture 100%
    // of transactions for performance monitoring.
    // We recommend adjusting this value in production
    tracesSampleRate: 1.0,
    release: "1-0-0",
    environment: "development-test",
  });
}

function log(error) {
  Sentry.captureException(error);
}

export default {
  init,
  log,
};
